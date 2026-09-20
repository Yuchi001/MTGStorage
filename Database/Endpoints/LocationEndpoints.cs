using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MTGStorage.Database.DataObjects;

namespace MTGStorage.Database.Endpoints
{
    public static class LocationEndpoints
    {
        private const string CardColumns = @"
            c.ID,
            c.Name,
            c.PrintID,
            c.ImageUrl,
            c.Count,
            c.LocationID,
            c.Price,
            c.Rank,
            c.OracleText,
            c.Colors,
            c.ColorIdentity,
            c.ManaCost,
            c.ConvertedManaCost,
            c.Types";

        private static Card ReadCard(System.Data.Common.DbDataReader reader)
        {
            return new Card
            {
                ID = reader.GetInt32(0),
                Name = reader.GetString(1),
                PrintID = reader.IsDBNull(2) ? "" : reader.GetString(2),
                ImageUrl = reader.IsDBNull(3) ? "" : reader.GetString(3),
                Count = reader.GetInt32(4),
                LocationID = reader.GetInt32(5),
                Price = reader.IsDBNull(6) ? 0m : reader.GetDecimal(6),
                Rank = reader.IsDBNull(7) ? 0 : reader.GetInt32(7),
                OracleText = reader.IsDBNull(8) ? "" : reader.GetString(8),
                Colors = reader.IsDBNull(9) ? "" : reader.GetString(9),
                ColorIdentity = reader.IsDBNull(10) ? "" : reader.GetString(10),
                ManaCost = reader.IsDBNull(11) ? "" : reader.GetString(11),
                ConvertedManaCost = reader.IsDBNull(12) ? 0 : reader.GetInt32(12),
                Types = reader.IsDBNull(13)
                    ? new List<string>()
                    : new List<string>(
                        reader.GetString(13)
                            .Split(
                                new[] { ',' },
                                StringSplitOptions.RemoveEmptyEntries))
            };
        }

        public static async Task RemoveLocation(int id)
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        DELETE FROM Location
                        WHERE ID = @id;
                    ";

                    command.Parameters.AddWithValue("@id", id);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<List<Location>> GetLocations()
        {
            var locations = new List<Location>();

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                        SELECT
                            l.ID,
                            l.Code,
                            l.Capacity,
                            l.MinPrice,
                            {CardColumns}
                        FROM Location l
                        LEFT JOIN Card c
                            ON c.LocationID = l.ID
                        ORDER BY
                            l.ID,
                            c.ID;
                    ";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        Location currentLocation = null;

                        while (await reader.ReadAsync())
                        {
                            int locationId = reader.GetInt32(0);

                            if (currentLocation == null ||
                                currentLocation.ID != locationId)
                            {
                                currentLocation = new Location
                                {
                                    ID = locationId,
                                    Code = reader.GetString(1),
                                    Capacity = reader.GetInt32(2),
                                    MinPrice = reader.GetDecimal(3)
                                };

                                locations.Add(currentLocation);
                            }

                            // c.ID znajduje się w kolumnie 4.
                            // Przy LEFT JOIN będzie NULL, jeżeli lokalizacja nie ma kart.
                            if (!reader.IsDBNull(4))
                            {
                                currentLocation.Cards.Add(
                                    ReadCard(reader, 4));
                            }
                        }
                    }
                }
            }

            await CardEndpoints.PopulateCardFaces(
                locations.SelectMany(location => location.Cards));

            return locations;
        }

        public static async Task AddLocation(Location location)
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        INSERT INTO Location (Code, Capacity, MinPrice)
                        VALUES (@code, @capacity, @minPrice);
                    ";

                    command.Parameters.AddWithValue("@code", location.Code);
                    command.Parameters.AddWithValue("@capacity", location.Capacity);
                    command.Parameters.AddWithValue("@minPrice", location.MinPrice);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public static async Task<List<Card>> GetCardsInLocation(int locationId)
        {
            var cards = new List<Card>();

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                        SELECT {CardColumns}
                        FROM Card c
                        WHERE c.LocationID = @locationID
                        ORDER BY c.ID;
                    ";

                    command.Parameters.AddWithValue("@locationID", locationId);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cards.Add(ReadCard(reader));
                        }
                    }
                }
            }

            await CardEndpoints.PopulateCardFaces(cards);
            return cards;
        }

        public static async Task<Card> GetCardInLocation(
            int locationId,
            string name,
            string printID = "")
        {
            Card card = null;

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                        SELECT {CardColumns}
                        FROM Card c
                        WHERE c.LocationID = @locationID
                          AND c.Name = @name
                          AND (c.PrintID = @printID OR @printID = '');
                    ";

                    command.Parameters.AddWithValue("@locationID", locationId);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@printID", printID);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync())
                            return null;

                        card = ReadCard(reader);
                    }
                }
            }

            await CardEndpoints.PopulateCardFaces(new[] { card });
            return card;
        }

        public static async Task<bool> CanAddLocation(string code)
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT 1
                        FROM Location
                        WHERE Code = @code;
                    ";

                    command.Parameters.AddWithValue("@code", code);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        return !await reader.ReadAsync();
                    }
                }
            }
        }

        public static async Task<(Location location, int maxCount)> FindLocation(Card card)
        {
            var location = FindLocation(await GetLocations(), card);
            return (location, location == null ? 0 : (int)FreeSpace(location));
        }

        internal static long FreeSpace(Location location) => location.Capacity - location.Cards.Sum(c => (long)c.Count);

        // Shared by adding cards, manual relocation and automatic relocation.
        internal static List<Location> MatchingLocations(IEnumerable<Location> locations, Card card, int? excludedId = null)
        {
            var available = locations.Where(l => l.ID != excludedId && l.MinPrice <= card.Price && FreeSpace(l) > 0).ToList();
            if (available.Count == 0) return new List<Location>();
            var bestMinimum = available.Max(l => l.MinPrice);
            return available.Where(l => l.MinPrice == bestMinimum &&
                    (l.Cards.Any(c => c.Name == card.Name && c.Count > 0) ||
                     l.Cards.Sum(c => (long)c.Count) / (decimal)l.Capacity < 0.75m))
                .OrderByDescending(l => l.Cards.Any(c => c.Name == card.Name && c.Count > 0))
                .ThenByDescending(l => l.Cards.Sum(c => (long)c.Count) / (decimal)l.Capacity)
                .ThenBy(l => l.ID).ToList();
        }

        internal static Location FindLocation(IEnumerable<Location> locations, Card card, int? excludedId = null)
            => MatchingLocations(locations, card, excludedId).FirstOrDefault();

        public static async Task<Location> GetLocation(int locationID)
        {
            Location location = null;

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                        SELECT
                            l.ID,
                            l.Code,
                            l.Capacity,
                            l.MinPrice,
                            {CardColumns}
                        FROM Location l
                        LEFT JOIN Card c
                            ON c.LocationID = l.ID
                        WHERE l.ID = @locationId
                        ORDER BY c.ID;
                    ";

                    command.Parameters.AddWithValue("@locationId", locationID);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            if (location == null)
                            {
                                location = new Location
                                {
                                    ID = reader.GetInt32(0),
                                    Code = reader.GetString(1),
                                    Capacity = reader.GetInt32(2),
                                    MinPrice = reader.GetDecimal(3)
                                };
                            }

                            // c.ID
                            if (!reader.IsDBNull(4))
                            {
                                location.Cards.Add(
                                    ReadCard(reader, 4));
                            }
                        }
                    }
                }
            }

            if (location != null)
            {
                await CardEndpoints.PopulateCardFaces(location.Cards);
            }

            return location;
        }

        public static async Task<bool> HasFreeLocation()
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT 1
                        FROM Location l
                        LEFT JOIN Card c
                            ON c.LocationID = l.ID
                        GROUP BY l.ID, l.Capacity
                        HAVING COALESCE(SUM(c.Count), 0) < l.Capacity
                        LIMIT 1;
                    ";

                    var result = await command.ExecuteScalarAsync();

                    return result != null;
                }
            }
        }

        public static async Task<bool> CanAddToLocation(int locationID)
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = @"
                        SELECT
                            l.Capacity - COALESCE(SUM(c.Count), 0) AS FreeSpace
                        FROM Location l
                        LEFT JOIN Card c
                            ON c.LocationID = l.ID
                        WHERE l.ID = @locationId
                        GROUP BY l.ID, l.Capacity;
                    ";

                    command.Parameters.AddWithValue("@locationId", locationID);

                    var result = await command.ExecuteScalarAsync();

                    if (result == null)
                        return false;

                    return Convert.ToInt32(result) > 0;
                }
            }
        }

        // Czyta Card z podanym offsetem kolumn.
        // Jest potrzebne, ponieważ GetLocations/GetLocation mają
        // najpierw 4 kolumny Location, a dopiero potem kolumny Card.
        private static Card ReadCard(
            System.Data.Common.DbDataReader reader,
            int offset)
        {
            return new Card
            {
                ID = reader.GetInt32(offset),
                Name = reader.GetString(offset + 1),
                PrintID = reader.IsDBNull(offset + 2)
                    ? ""
                    : reader.GetString(offset + 2),
                ImageUrl = reader.IsDBNull(offset + 3)
                    ? ""
                    : reader.GetString(offset + 3),
                Count = reader.GetInt32(offset + 4),
                LocationID = reader.GetInt32(offset + 5),
                Price = reader.IsDBNull(offset + 6)
                    ? 0m
                    : reader.GetDecimal(offset + 6),
                Rank = reader.IsDBNull(offset + 7)
                    ? 0
                    : reader.GetInt32(offset + 7),
                OracleText = reader.IsDBNull(offset + 8)
                    ? ""
                    : reader.GetString(offset + 8),
                Colors = reader.IsDBNull(offset + 9)
                    ? ""
                    : reader.GetString(offset + 9),
                ColorIdentity = reader.IsDBNull(offset + 10)
                    ? ""
                    : reader.GetString(offset + 10),
                ManaCost = reader.IsDBNull(offset + 11)
                    ? ""
                    : reader.GetString(offset + 11),
                ConvertedManaCost = reader.IsDBNull(offset + 12)
                    ? 0
                    : reader.GetInt32(offset + 12),
                Types = reader.IsDBNull(offset + 13)
                    ? new List<string>()
                    : new List<string>(
                        reader.GetString(offset + 13)
                            .Split(
                                new[] { ',' },
                                StringSplitOptions.RemoveEmptyEntries))
            };
        }
    }
}
