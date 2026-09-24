using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using MTGStorage;
using MTGStorage.Features;
using MTGStorage.Database.DataObjects;
using MTGStorage.Database.Endpoints;

internal static class BulkImportTests
{
    private static void Assert(bool condition, string message)
    {
        if (!condition) throw new Exception(message);
    }

    private static void Reject(Action action)
    {
        try { action(); } catch (FormatException) { return; }
        throw new Exception("Expected invalid input to be rejected.");
    }

    [STAThread]
    private static void Main()
    {
        var entries = BulkCardImport.Parse(new[] { "", "  Sol Ring  2 ", "Lightning Bolt\t3", "  Counterspell  ", "Sol Ring", "Lightning Bolt M11 3" });
        Assert(entries.Count == 5 && entries[0].Name == "Sol Ring" && entries[0].Count == 2 &&
            entries[1].Name == "Lightning Bolt" && entries[1].Count == 3, "TXT parsing");
        Assert(entries[2].Name == "Counterspell" && entries[2].Count == 1 &&
            entries[3].Name == "Sol Ring" && entries[3].Count == 1, "Missing count defaults to one");
        Assert(entries[4].Name == "Lightning Bolt M11", "Set codes must remain part of the name, not be interpreted");
        foreach (var bad in new[] { "Sol Ring 0", "Sol Ring -1", "Sol Ring 2147483648", "" })
            Reject(() => BulkCardImport.Parse(new[] { bad }));
        var card = new Card { Name = "Sol Ring", PrintID = "", Price = 2m };
        var locations = new[] { new Location { ID = 1, Code = "A", Capacity = 3 }, new Location { ID = 2, Code = "B", Capacity = 5 } };
        var plan = BulkCardImport.BuildPlan(new[] { new KeyValuePair<Card,int>(card, 4), new KeyValuePair<Card,int>(card, 2) }, locations);
        Assert(plan.Sum(p => p.Count) == 6 && plan.Where(p => p.Location.ID == 1).Sum(p => p.Count) == 3, "Split/reservation");
        Assert(locations.All(l => l.Cards.Count == 0), "Planning must not mutate storage");
        Assert(plan.All(p => p.Owned == 0), "Pending import copies must not count as owned");
        var ownedPlan = BulkCardImport.BuildPlan(new[] { new KeyValuePair<Card,int>(card, 3), new KeyValuePair<Card,int>(card, 1) },
            new[] { new Location { ID=1, Capacity=10, Cards = new List<Card> {
                new Card { Name="Sol Ring", PrintID="A", Count=2 },
                new Card { Name="Sol Ring", PrintID="B", Count=3 },
                new Card { Name="Other", Count=1 } } } });
        Assert(ownedPlan.All(p => p.Owned == 5), "Owned sums existing copies across prints without pending copies");
        bool full = false;
        try { BulkCardImport.BuildPlan(new[] { new KeyValuePair<Card,int>(card, 9) }, locations); }
        catch (InvalidOperationException) { full = true; }
        Assert(full, "Capacity overflow must fail");
        var tierPlan = BulkCardImport.BuildPlan(new[] { new KeyValuePair<Card,int>(card, 1) }, new[] {
            new Location { ID=1, Capacity=10, MinPrice=0 }, new Location { ID=2, Capacity=10, MinPrice=1 },
            new Location { ID=3, Capacity=10, MinPrice=3 } });
        Assert(tierPlan[0].Location.ID == 2, "Existing price tier rules");
        using (var window = new CardBulkShipmentWindow())
        {
            window.InitImport(plan);
            var flags = BindingFlags.Instance | BindingFlags.NonPublic;
            var grid = (DataGridView)typeof(CardBulkShipmentWindow).GetField("cardLocationPairTable", flags).GetValue(window);
            var complete = (Button)typeof(CardBulkShipmentWindow).GetField("confirmButton", flags).GetValue(window);
            var changed = typeof(CardBulkShipmentWindow).GetMethod("cardLocationPairTable_CellEndEdit", flags);
            Assert(grid.Columns[grid.Columns.Count - 1].Name == "image" && grid.Columns.Contains("price"), "Columns");
            Assert(grid.Columns[1].Name == "owned" && !grid.Columns.Contains("set"), "Owned replaces Set in import");
            Assert((long)grid.Rows[0].Cells["owned"].Value == plan[0].Owned, "Owned value displayed");
            Assert(!complete.Enabled && complete.Text == "Complete", "Initially disabled");
            foreach (DataGridViewRow row in grid.Rows) row.Cells["completed"].Value = true;
            changed.Invoke(window, new object[] { grid, null });
            Assert(complete.Enabled, "All completed enables button");
            grid.Rows[0].Cells["completed"].Value = false;
            changed.Invoke(window, new object[] { grid, null });
            Assert(!complete.Enabled, "Unchecking disables button");
            var selectedMethod = typeof(CardBulkShipmentWindow).GetMethod("GetImportPlacementsToSave", flags);
            grid.Rows[0].Cells["remove"].Value = true;
            changed.Invoke(window, new object[] { grid, null });
            Assert(complete.Enabled, "Removed rows do not require Completed");
            var selected = (List<BulkCardImport.Placement>)selectedMethod.Invoke(window, null);
            Assert(selected.Count == plan.Count - 1 && !selected.Contains(plan[0]), "Removed rows excluded from save");
            grid.Sort(grid.Columns["count"], System.ComponentModel.ListSortDirection.Descending);
            selected = (List<BulkCardImport.Placement>)selectedMethod.Invoke(window, null);
            Assert(!selected.Contains(plan[0]), "Removal remains attached to placement after sorting");
            foreach (DataGridViewRow row in grid.Rows) row.Cells["remove"].Value = true;
            changed.Invoke(window, new object[] { grid, null });
            selected = (List<BulkCardImport.Placement>)selectedMethod.Invoke(window, null);
            Assert(complete.Enabled && selected.Count == 0, "All removed can finish without saving cards");
        }
        TestDatabase().GetAwaiter().GetResult();
        Console.WriteLine("PASS: parsing, validation, capacity reservations, price tiers, completion gating, atomic save, rollback, print separation and card faces.");
    }

    private static async Task TestDatabase()
    {
        using (var connection = new SQLiteConnection("Data Source=:memory:;Version=3;Foreign Keys=True;"))
        {
            connection.Open();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"CREATE TABLE Location(ID INTEGER PRIMARY KEY, Code TEXT, Capacity INTEGER, MinPrice NUMERIC);
                    CREATE TABLE Card(ID INTEGER PRIMARY KEY, Name TEXT, PrintID TEXT, ImageUrl TEXT, Count INTEGER,
                    LocationID INTEGER REFERENCES Location(ID), Price NUMERIC, Rank INTEGER, OracleText TEXT,
                    Colors TEXT, ColorIdentity TEXT, ManaCost TEXT, ConvertedManaCost INTEGER, Types TEXT);
                    CREATE TABLE CardFace(ID INTEGER PRIMARY KEY, CardID INTEGER REFERENCES Card(ID), Name TEXT,
                    OracleText TEXT, ImageUrl TEXT, Colors TEXT, ManaCost TEXT, Types TEXT);
                    INSERT INTO Location VALUES(1,'A',10,0);";
                cmd.ExecuteNonQuery();
            }
            var card = new Card { Name="Test", PrintID="Set /1", Price=1m,
                CardFaces = new List<CardFace> { new CardFace { Name="Face" } } };
            var location = new Location { ID=1, Code="A" };
            var valid = new BulkCardImport.Placement { Card=card, Location=location, Count=2 };
            var invalid = new BulkCardImport.Placement { Card=card, Location=location, Count=20 };
            var save = typeof(CardEndpoints).GetMethod("AddCardsBulk", BindingFlags.Static | BindingFlags.NonPublic);
            bool failed = false;
            try { await (Task)save.Invoke(null, new object[] { connection, new[] { valid, invalid } }); }
            catch (InvalidOperationException) { failed = true; }
            Assert(failed && Scalar(connection,"SELECT COUNT(*) FROM Card") == 0 && Scalar(connection,"SELECT COUNT(*) FROM CardFace") == 0, "Entire transaction rolls back");
            await (Task)save.Invoke(null, new object[] { connection, new[] { valid, valid } });
            Assert(Scalar(connection,"SELECT SUM(Count) FROM Card") == 4 && Scalar(connection,"SELECT COUNT(*) FROM Card") == 1, "Merge quantities");
            Assert(Scalar(connection,"SELECT COUNT(*) FROM CardFace") == 1, "Save faces once");
            var unspecified = new BulkCardImport.Placement { Card=new Card { Name="Test", PrintID="", Price=1 }, Location=location, Count=1 };
            await (Task)save.Invoke(null, new object[] { connection, new[] { unspecified } });
            Assert(Scalar(connection,"SELECT COUNT(*) FROM Card") == 2, "Unspecified print must not merge with specific print");
        }
    }
    private static long Scalar(SQLiteConnection connection, string sql)
    {
        using (var cmd = connection.CreateCommand()) { cmd.CommandText = sql; return Convert.ToInt64(cmd.ExecuteScalar()); }
    }
}
