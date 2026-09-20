using System;
using System.IO;
using System.IO.Compression;
using System.Data.SQLite;
using System.Reflection;
using System.Collections.Generic;

class ImportRegression
{
    static Type Endpoint = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MTGStorage.exe")).GetType("MTGStorage.Database.Endpoints.ExportImportEndpoints");
    static string[] Tables = { "Location", "Card", "CardFace" };
    static string[] Files = { "Locations.csv", "Cards.csv", "CardFaces.csv" };
    static object Call(string name, params object[] args)
    {
        try { return Endpoint.GetMethod(name, BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, args); }
        catch (TargetInvocationException e) { throw e.InnerException; }
    }
    static void Assert(bool value, string message) { if (!value) throw new Exception(message); }
    static byte[] Export(SQLiteConnection connection)
    {
        using (var stream = new MemoryStream())
        {
            using (var transaction = connection.BeginTransaction())
            using (var zip = new ZipArchive(stream, ZipArchiveMode.Create, true))
            {
                for (int i = 0; i < 3; i++) Call("ExportTable", connection, transaction, zip, Tables[i], Files[i]);
                transaction.Commit();
            }
            return stream.ToArray();
        }
    }
    static string Entry(byte[] data, string name)
    {
        using (var stream = new MemoryStream(data))
        using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
        using (var reader = new StreamReader(zip.GetEntry(name).Open())) return reader.ReadToEnd();
    }
    static void RoundTrip(SQLiteConnection source, int expectedRecovery)
    {
        var before = Export(source);
        var data = new object[3];
        using (var stream = new MemoryStream(before))
        using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
            for (int i = 0; i < 3; i++) data[i] = Call("ReadCsvEntry", zip, Files[i]);
        using (var target = new SQLiteConnection("Data Source=:memory:;Foreign Keys=True;"))
        {
            target.Open();
            var schema = new List<string>();
            using (var cmd = source.CreateCommand())
            {
                cmd.CommandText = "SELECT sql FROM sqlite_master WHERE type='table' AND name IN ('Location','Card','CardFace')";
                using (var reader = cmd.ExecuteReader()) while (reader.Read()) schema.Add(reader.GetString(0));
            }
            foreach (var sql in schema) using (var cmd = target.CreateCommand()) { cmd.CommandText = sql; cmd.ExecuteNonQuery(); }
            var columns = new object[3];
            for (int i = 0; i < 3; i++)
            {
                columns[i] = Call("GetTableColumns", target, Tables[i]);
                Call("ValidateTable", data[i], columns[i], Files[i]);
            }
            if (expectedRecovery > 0)
            {
                try { Call("ValidateRelationships", data); throw new Exception("Expected missing location"); }
                catch (InvalidDataException e) { Assert(e.Message.Contains("Cards.csv row 2") && e.Message.Contains("(1)"), e.Message); }
            }
            Assert((int)Call("RecoverMissingLocations", data[0], data[1]) == expectedRecovery, "Recovery count");
            Assert((int)Call("RecoverMissingLocations", data[0], data[1]) == 0, "Recovery must be idempotent");
            Call("ValidateTable", data[0], columns[0], Files[0]);
            Call("ValidateRelationships", data);
            var faceRows = (List<string[]>)data[2].GetType().GetProperty("Rows").GetValue(data[2], null);
            if (faceRows.Count > 0)
            {
                var headers = (string[])data[2].GetType().GetProperty("Headers").GetValue(data[2], null);
                var cardIndex = Array.IndexOf(headers, "CardID");
                var original = faceRows[0][cardIndex];
                faceRows[0][cardIndex] = long.MinValue.ToString();
                try { Call("ValidateRelationships", data); throw new Exception("Expected invalid CardID rejection"); }
                catch (InvalidDataException e) { Assert(e.Message.Contains("CardFaces.csv row 2"), e.Message); }
                faceRows[0][cardIndex] = original;
                faceRows.Add(faceRows[0]);
                try { Call("ValidateTable", data[2], columns[2], Files[2]); throw new Exception("Expected duplicate ID rejection"); }
                catch (InvalidDataException e) { Assert(e.Message.Contains("duplicate IDs"), e.Message); }
                faceRows.RemoveAt(faceRows.Count - 1);
            }
            using (var tx = target.BeginTransaction())
            {
                Call("DeleteExistingData", target, tx);
                for (int i = 0; i < 3; i++) Call("InsertTable", target, tx, Tables[i], columns[i], data[i]);
                tx.Commit();
            }
            var after = Export(target);
            for (int i = expectedRecovery == 0 ? 0 : 1; i < 3; i++)
                Assert(Entry(before, Files[i]) == Entry(after, Files[i]), "Round trip changed " + Files[i]);
            using (var cmd = target.CreateCommand())
            {
                cmd.CommandText = "SELECT COUNT(*) FROM Card WHERE LocationID NOT IN (SELECT ID FROM Location)";
                Assert(Convert.ToInt32(cmd.ExecuteScalar()) == 0, "Orphan cards remain");
            }
            if (expectedRecovery > 0) RoundTrip(target, 0);
        }
    }
    static void Main(string[] args)
    {
        using (var fixture = new SQLiteConnection("Data Source=:memory:;Foreign Keys=True;"))
        {
            fixture.Open();
            using (var cmd = fixture.CreateCommand())
            {
                cmd.CommandText = "CREATE TABLE Location(ID INTEGER PRIMARY KEY,Code TEXT NOT NULL,Capacity INTEGER NOT NULL,MinPrice NUMERIC NOT NULL);" +
                    "CREATE TABLE Card(ID INTEGER PRIMARY KEY,LocationID INTEGER NOT NULL REFERENCES Location(ID),Count INTEGER NOT NULL,Name TEXT,Price NUMERIC);" +
                    "CREATE TABLE CardFace(ID INTEGER PRIMARY KEY,CardID INTEGER NOT NULL REFERENCES Card(ID),Name TEXT);" +
                    "INSERT INTO Location VALUES(7,'Box',10,0); INSERT INTO Card VALUES(16,7,2,'Comma, quote \" and\nnew line ą',1.23); INSERT INTO CardFace VALUES(99,16,'Face');";
                cmd.ExecuteNonQuery();
            }
            RoundTrip(fixture, 0);
            Console.WriteLine("PASS: valid ZIP round trip, nonsequential IDs, CSV escaping, Unicode, decimals and faces");
        }
        if (args.Length > 0) using (var source = new SQLiteConnection("Data Source=" + args[0] + ";Read Only=True;"))
        {
            source.Open(); RoundTrip(source, 1);
            Console.WriteLine("PASS: real database export/import, missing location recovery, all cards/faces preserved, second round trip");
        }
    }
}
