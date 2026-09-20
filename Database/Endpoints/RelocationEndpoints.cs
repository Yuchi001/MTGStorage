using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MTGStorage.Database.DataObjects;

namespace MTGStorage.Database.Endpoints
{
    public static class RelocationEndpoints
    {
        public static Task<List<RelocatedCard>> Relocate(int sourceId, int targetId, IDictionary<int, int> quantities, bool allCards = false)
        {
            return Task.Run(() =>
            {
                using (var connection = Database.GetConnection())
                {
                    connection.Open();
                    return Relocate(connection, sourceId, targetId, quantities, allCards);
                }
            });
        }

        internal static List<RelocatedCard> Relocate(SQLiteConnection connection, int sourceId, int targetId,
            IDictionary<int, int> quantities, bool allCards)
        {
            if (sourceId == targetId) throw new InvalidOperationException("Choose a different location.");
            using (var tx = connection.BeginTransaction())
            using (var cmd = connection.CreateCommand())
            {
                cmd.Transaction = tx;
                cmd.Parameters.AddWithValue("@source", sourceId);
                cmd.Parameters.AddWithValue("@target", targetId);
                cmd.CommandText = "SELECT ID, Count FROM Card WHERE LocationID=@source";
                var stock = new Dictionary<int, int>();
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read()) stock.Add(reader.GetInt32(0), reader.GetInt32(1));
                var moves = allCards ? stock.Where(p => p.Value > 0).ToDictionary(p => p.Key, p => p.Value)
                    : new Dictionary<int, int>(quantities);
                if (moves.Count == 0 || moves.Any(p => p.Value <= 0 || !stock.ContainsKey(p.Key) || stock[p.Key] < p.Value))
                    throw new InvalidOperationException("The selected quantities are no longer available. Refresh the location.");
                var locations = ReadLocations(cmd);
                var plan = BuildPlan(locations, sourceId, targetId, moves);
                var cardColumns = Columns(cmd, "Card", "ID");
                var faceColumns = Columns(cmd, "CardFace", "ID");
                cmd.Parameters.AddWithValue("@id", 0);
                cmd.Parameters.AddWithValue("@count", 0);
                cmd.Parameters.AddWithValue("@newId", 0L);
                foreach (var move in plan)
                {
                    cmd.Parameters["@target"].Value = move.TargetId;
                    cmd.Parameters["@id"].Value = move.CardId;
                    cmd.Parameters["@count"].Value = move.Count;
                    if (move.Count == stock[move.CardId])
                    {
                        cmd.CommandText = "UPDATE Card SET LocationID=@target WHERE ID=@id";
                        cmd.ExecuteNonQuery();
                        continue;
                    }
                    cmd.CommandText = "INSERT INTO Card (" + Join(cardColumns) + ") SELECT " +
                        string.Join(",", cardColumns.Select(c => c == "Count" ? "@count" : c == "LocationID" ? "@target" : "[" + c + "]")) +
                        " FROM Card WHERE ID=@id; SELECT last_insert_rowid();";
                    cmd.Parameters["@newId"].Value = Convert.ToInt64(cmd.ExecuteScalar());
                    cmd.CommandText = "INSERT INTO CardFace (" + Join(faceColumns) + ") SELECT " +
                        string.Join(",", faceColumns.Select(c => c == "CardID" ? "@newId" : "[" + c + "]")) +
                        " FROM CardFace WHERE CardID=@id";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "UPDATE Card SET Count=Count-@count WHERE ID=@id";
                    cmd.ExecuteNonQuery();
                    stock[move.CardId] -= move.Count;
                }
                tx.Commit();
                return plan;
            }
        }
        public sealed class RelocatedCard
        {
            public int CardId { get; set; }
            public string Name { get; set; }
            public string PrintID { get; set; }
            public int Count { get; set; }
            public int TargetId { get; set; }
            public string TargetCode { get; set; }
        }

        public static List<Location> ManualDestinations(List<Location> locations, int sourceId, IDictionary<int,int> moves)
        {
            var source = locations.Single(l => l.ID == sourceId);
            var total = moves.Sum(m => (long)m.Value);
            return locations.Where(l => l.ID != sourceId && LocationEndpoints.FreeSpace(l) >= total &&
                moves.All(m => LocationEndpoints.MatchingLocations(locations, source.Cards.Single(c => c.ID == m.Key), sourceId)
                    .Any(candidate => candidate.ID == l.ID))).ToList();
        }

        internal static List<RelocatedCard> BuildPlan(List<Location> locations, int sourceId, int targetId, IDictionary<int,int> moves)
        {
            var source = locations.Single(l => l.ID == sourceId);
            if (targetId != 0 && !ManualDestinations(locations, sourceId, moves).Any(l => l.ID == targetId))
                throw new InvalidOperationException("The destination does not match the cards' price tier or has insufficient space.");
            var plan = new List<RelocatedCard>();
            foreach (var move in moves)
            {
                var card = source.Cards.Single(c => c.ID == move.Key);
                var remaining = move.Value;
                while (remaining > 0)
                {
                    var target = targetId == 0 ? LocationEndpoints.FindLocation(locations, card, sourceId)
                        : locations.Single(l => l.ID == targetId);
                    if (target == null) throw new InvalidOperationException("No matching location for " + card.Name + ". No cards have been moved.");
                    var count = (int)Math.Min(remaining, LocationEndpoints.FreeSpace(target));
                    if (count <= 0) throw new InvalidOperationException("The destination has insufficient space.");
                    plan.Add(new RelocatedCard { CardId = card.ID, Name = card.Name, PrintID = card.PrintID,
                        Count = count, TargetId = target.ID, TargetCode = target.Code });
                    target.Cards.Add(new Card { Name = card.Name, Count = count, Price = card.Price });
                    remaining -= count;
                }
            }
            return plan;
        }

        private static List<Location> ReadLocations(SQLiteCommand cmd)
        {
            var locations = new List<Location>();
            cmd.CommandText = "SELECT ID,Code,Capacity,MinPrice FROM Location";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read()) locations.Add(new Location { ID=reader.GetInt32(0),Code=reader.GetString(1),Capacity=reader.GetInt32(2),MinPrice=reader.GetDecimal(3) });
            cmd.CommandText = "SELECT ID,LocationID,Name,PrintID,Count,Price FROM Card";
            using (var reader = cmd.ExecuteReader())
                while (reader.Read())
                {
                    var location = locations.FirstOrDefault(l => l.ID == reader.GetInt32(1));
                    if (location != null) location.Cards.Add(new Card { ID=reader.GetInt32(0),Name=reader.GetString(2),PrintID=reader.IsDBNull(3)?"":reader.GetString(3),Count=reader.GetInt32(4),Price=reader.IsDBNull(5)?0m:reader.GetDecimal(5) });
                }
            return locations;
        }

        private static List<string> Columns(SQLiteCommand cmd, string table, string exclude)
        {
            cmd.CommandText = "PRAGMA table_info([" + table + "])";
            var columns = new List<string>();
            using (var reader = cmd.ExecuteReader())
                while (reader.Read()) if (reader.GetString(1) != exclude) columns.Add(reader.GetString(1));
            return columns;
        }
        private static string Join(IEnumerable<string> columns) { return string.Join(",", columns.Select(c => "[" + c + "]")); }
    }
}
