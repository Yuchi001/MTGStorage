using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using MTGStorage.Database.DataObjects;
using MTGStorage.Features;

namespace MTGStorage.Database.Endpoints
{
    public static class CardEndpoints
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
            c.Types,
            l.ID,
            l.Code,
            l.Capacity,
            l.MinPrice";

        public static Card ReadCard(System.Data.Common.DbDataReader reader)
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

                OracleText = reader.IsDBNull(8)
                    ? ""
                    : reader.GetString(8),

                Colors = reader.IsDBNull(9)
                    ? ""
                    : reader.GetString(9),

                ColorIdentity = reader.IsDBNull(10)
                    ? ""
                    : reader.GetString(10),

                ManaCost = reader.IsDBNull(11)
                    ? ""
                    : reader.GetString(11),

                ConvertedManaCost = reader.IsDBNull(12)
                    ? 0
                    : reader.GetInt32(12),

                Types = reader.IsDBNull(13)
                    ? new List<string>()
                    : reader.GetString(13)
                        .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(e => e.Trim())
                        .ToList(),

                Location = reader.IsDBNull(14)
                    ? null
                    : new Location
                    {
                        ID = reader.GetInt32(14),
                        Code = reader.IsDBNull(15)
                            ? ""
                            : reader.GetString(15),
                        Capacity = reader.IsDBNull(16)
                            ? 0
                            : reader.GetInt32(16),
                        MinPrice = reader.IsDBNull(17)
                            ? 0
                            : reader.GetDecimal(17)
                    }
            };
        }

        private static string SerializeTypes(List<string> types)
        {
            return types == null
                ? ""
                : string.Join(",", types);
        }

        private static string CreateColorCondition(
            string columnName,
            List<string> parameterNames,
            bool wantsColorless,
            SearchOptions.EColorsSearchType searchType)
        {
            var colorConditions = new List<string>();

            if (wantsColorless)
            {
                colorConditions.Add($"({columnName} IS NULL OR {columnName} = '')");
            }

            foreach (var parameterName in parameterNames)
            {
                colorConditions.Add(
                    $"(',' || {columnName} || ',') LIKE '%,' || {parameterName} || ',%'");
            }

            if (searchType == SearchOptions.EColorsSearchType.ANY)
            {
                return "(" + string.Join(" OR ", colorConditions) + ")";
            }

            if (wantsColorless)
            {
                return colorConditions[0];
            }

            var requiredColors = "(" + string.Join(" AND ", colorConditions) + ")";

            if (searchType != SearchOptions.EColorsSearchType.EXACT)
            {
                return requiredColors;
            }

            return $@"
                (
                    {requiredColors}
                    AND
                    (
                        CASE
                            WHEN {columnName} IS NULL OR {columnName} = '' THEN 0
                            ELSE LENGTH({columnName})
                                - LENGTH(REPLACE({columnName}, ',', '')) + 1
                        END
                    ) = {parameterNames.Count}
                )";
        }

        public static async Task PopulateCardFaces(IEnumerable<Card> cards)
        {
            var cardsById = cards
                .Where(card => card != null)
                .GroupBy(card => card.ID)
                .ToDictionary(group => group.Key, group => group.ToList());

            foreach (var cardGroup in cardsById.Values)
            {
                foreach (var card in cardGroup)
                {
                    card.CardFaces = new List<CardFace>();
                }
            }

            if (cardsById.Count == 0)
            {
                return;
            }

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                        SELECT ID, CardID, Name, OracleText, ImageUrl, Colors, ManaCost, Types
                        FROM CardFace
                        WHERE CardID IN ({string.Join(",", cardsById.Keys)});
                    ";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var cardID = reader.GetInt32(1);
                            var types = reader.IsDBNull(7)
                                ? new List<string>()
                                : reader.GetString(7)
                                    .Split(
                                        new[] { ',' },
                                        StringSplitOptions.RemoveEmptyEntries)
                                    .Select(type => type.Trim())
                                    .ToList();

                            foreach (var card in cardsById[cardID])
                            {
                                card.CardFaces.Add(new CardFace
                                {
                                    ID = reader.GetInt32(0),
                                    CardID = cardID,
                                    Name = reader.IsDBNull(2) ? "" : reader.GetString(2),
                                    OracleText = reader.IsDBNull(3) ? "" : reader.GetString(3),
                                    ImageUrl = reader.IsDBNull(4) ? "" : reader.GetString(4),
                                    Colors = reader.IsDBNull(5) ? "" : reader.GetString(5),
                                    ManaCost = reader.IsDBNull(6) ? "" : reader.GetString(6),
                                    Types = new List<string>(types),
                                    Card = card
                                });
                            }
                        }
                    }
                }
            }
        }

        public static async Task AddCard(Card card, int locationID, int count = 1)
        {
            var sameCardInLocation = await LocationEndpoints.GetCardInLocation(
                locationID,
                card.Name,
                card.PrintID);

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var transaction = connection.BeginTransaction())
                using (var command = connection.CreateCommand())
                {
                    command.Transaction = transaction;

                    await AddCard(command, card, locationID, count, sameCardInLocation);

                    transaction.Commit();
                }
            }
        }

        private static async Task AddCard(SQLiteCommand command, Card card, int locationID, int count, Card sameCardInLocation)
        {
            if (sameCardInLocation == null)
            {
                command.CommandText = @"
                    INSERT INTO Card
                    (
                        Name,
                        PrintID,
                        ImageUrl,
                        Count,
                        LocationID,
                        Price,
                        Rank,
                        OracleText,
                        Colors,
                        ColorIdentity,
                        ManaCost,
                        ConvertedManaCost,
                        Types
                    )
                    VALUES
                    (
                        @Name,
                        @PrintID,
                        @ImageUrl,
                        @Count,
                        @LocationID,
                        @Price,
                        @Rank,
                        @OracleText,
                        @Colors,
                        @ColorIdentity,
                        @ManaCost,
                        @ConvertedManaCost,
                        @Types
                    );
                ";

                command.Parameters.AddWithValue(
                    "@Name",
                    card.Name);

                command.Parameters.AddWithValue(
                    "@PrintID",
                    card.PrintID ?? "");

                command.Parameters.AddWithValue(
                    "@ImageUrl",
                    card.ImageUrl ?? "");

                command.Parameters.AddWithValue(
                    "@Count",
                    count);

                command.Parameters.AddWithValue(
                    "@LocationID",
                    locationID);

                command.Parameters.AddWithValue(
                    "@Price",
                    Math.Round(card.Price, 2));

                command.Parameters.AddWithValue(
                    "@Rank",
                    card.Rank);

                command.Parameters.AddWithValue(
                    "@OracleText",
                    card.OracleText ?? "");

                command.Parameters.AddWithValue(
                    "@Colors",
                    card.Colors ?? "");

                command.Parameters.AddWithValue(
                    "@ColorIdentity",
                    card.ColorIdentity ?? "");

                command.Parameters.AddWithValue(
                    "@ManaCost",
                    card.ManaCost ?? "");

                command.Parameters.AddWithValue(
                    "@ConvertedManaCost",
                    card.ConvertedManaCost);

                command.Parameters.AddWithValue(
                    "@Types",
                    SerializeTypes(card.Types));
            }
            else
            {
                command.CommandText = @"
                    UPDATE Card
                    SET
                        Count = @newCount,
                        Price = @price,
                        Rank = @rank,
                        OracleText = @OracleText,
                        Colors = @Colors,
                        ColorIdentity = @ColorIdentity,
                        ManaCost = @ManaCost,
                        ConvertedManaCost = @ConvertedManaCost,
                        Types = @Types
                    WHERE ID = @cardID;
                ";

                command.Parameters.AddWithValue(
                    "@newCount",
                    sameCardInLocation.Count + count);

                command.Parameters.AddWithValue(
                    "@price",
                    Math.Round(card.Price, 2));

                command.Parameters.AddWithValue(
                    "@rank",
                    card.Rank);

                command.Parameters.AddWithValue(
                    "@OracleText",
                    card.OracleText ?? "");

                command.Parameters.AddWithValue(
                    "@Colors",
                    card.Colors ?? "");

                command.Parameters.AddWithValue(
                    "@ColorIdentity",
                    card.ColorIdentity ?? "");

                command.Parameters.AddWithValue(
                    "@ManaCost",
                    card.ManaCost ?? "");

                command.Parameters.AddWithValue(
                    "@ConvertedManaCost",
                    card.ConvertedManaCost);

                command.Parameters.AddWithValue(
                    "@Types",
                    SerializeTypes(card.Types));

                command.Parameters.AddWithValue(
                    "@cardID",
                    sameCardInLocation.ID);
            }

            await command.ExecuteNonQueryAsync();

            if (sameCardInLocation == null &&
                card.CardFaces != null &&
                card.CardFaces.Count > 0)
            {
                command.Parameters.Clear();
                command.CommandText = "SELECT last_insert_rowid();";
                var cardID = Convert.ToInt32(await command.ExecuteScalarAsync());

                foreach (var cardFace in card.CardFaces)
                {
                    if (cardFace == null)
                    {
                        continue;
                    }

                    command.Parameters.Clear();
                    command.CommandText = @"
                        INSERT INTO CardFace
                        (
                            CardID,
                            Name,
                            OracleText,
                            ImageUrl,
                            Colors,
                            ManaCost,
                            Types
                        )
                        VALUES
                        (
                            @CardID,
                            @Name,
                            @OracleText,
                            @ImageUrl,
                            @Colors,
                            @ManaCost,
                            @Types
                        );
                    ";

                    command.Parameters.AddWithValue("@CardID", cardID);
                    command.Parameters.AddWithValue("@Name", cardFace.Name ?? "");
                    command.Parameters.AddWithValue(
                        "@OracleText",
                        cardFace.OracleText ?? "");
                    command.Parameters.AddWithValue(
                        "@ImageUrl",
                        cardFace.ImageUrl ?? "");
                    command.Parameters.AddWithValue(
                        "@Colors",
                        cardFace.Colors ?? "");
                    command.Parameters.AddWithValue(
                        "@ManaCost",
                        cardFace.ManaCost ?? "");
                    command.Parameters.AddWithValue(
                        "@Types",
                        SerializeTypes(cardFace.Types));

                    await command.ExecuteNonQueryAsync();
                }
            }

        }

        public static async Task AddCardsBulk(IReadOnlyList<BulkCardImport.Placement> placements)
        {
            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();
                await AddCardsBulk(connection, placements);
            }
        }

        internal static async Task AddCardsBulk(SQLiteConnection connection, IReadOnlyList<BulkCardImport.Placement> placements)
        {
            if (placements.Count == 0) throw new InvalidOperationException("No cards to add.");
            using (var transaction = connection.BeginTransaction())
            using (var command = connection.CreateCommand())
            {
                command.Transaction = transaction;
                foreach (var placement in placements)
                {
                    if (placement.Count <= 0) throw new InvalidOperationException("Count must be positive.");
                    command.Parameters.Clear();
                    command.Parameters.AddWithValue("@location", placement.Location.ID);
                    command.CommandText = @"SELECT l.Capacity - COALESCE(SUM(c.Count), 0), l.MinPrice
                        FROM Location l LEFT JOIN Card c ON c.LocationID=l.ID
                        WHERE l.ID=@location GROUP BY l.ID, l.Capacity, l.MinPrice";
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (!await reader.ReadAsync() || reader.GetInt64(0) < placement.Count || reader.GetDecimal(1) > placement.Card.Price)
                            throw new InvalidOperationException("Location " + placement.Location.Code + " is no longer available. Import the file again.");
                    }
                    command.Parameters.AddWithValue("@name", placement.Card.Name);
                    command.Parameters.AddWithValue("@print", placement.Card.PrintID ?? "");
                    command.CommandText = "SELECT ID, Count FROM Card WHERE LocationID=@location AND Name=@name AND PrintID=@print LIMIT 1";
                    Card existing = null;
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync()) existing = new Card { ID = reader.GetInt32(0), Count = reader.GetInt32(1) };
                    }
                    command.Parameters.Clear();
                    await AddCard(command, placement.Card, placement.Location.ID, placement.Count, existing);
                }
                transaction.Commit();
            }
        }
        public static async Task<List<Card>> GetCards()
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
                        LEFT JOIN Location l
                            ON c.LocationID = l.ID;
                    ";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cards.Add(ReadCard(reader));
                        }
                    }
                }
            }

            await PopulateCardFaces(cards);
            return cards;
        }

        public static async Task<List<Card>> GetCardsDistinct()
        {
            var cards = new List<Card>();

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = $@"
                        SELECT {CardColumns}
                        FROM
                        (
                            SELECT c.*,
                                   ROW_NUMBER() OVER (
                                       PARTITION BY c.Name
                                       ORDER BY c.Price ASC
                                   ) AS rn
                            FROM Card c
                        ) c
                        LEFT JOIN Location l
                            ON c.LocationID = l.ID
                        WHERE c.rn = 1;
                    ";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cards.Add(ReadCard(reader));
                        }
                    }
                }
            }

            await PopulateCardFaces(cards);
            return cards;
        }

        public static async Task<Card> GetCard(int cardID)
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
                        LEFT JOIN Location l
                            ON c.LocationID = l.ID
                        WHERE c.ID = @cardId;
                    ";

                    command.Parameters.AddWithValue(
                        "@cardId",
                        cardID);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                            card = ReadCard(reader);
                    }
                }
            }

            if (card != null)
            {
                await PopulateCardFaces(new[] { card });
            }

            return card;
        }

        public static async Task RemoveCard(int cardID, int count = 1)
        {
            var cardToRemove = await GetCard(cardID);

            if (cardToRemove == null)
                return;

            var newCount = cardToRemove.Count - count;

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    if (newCount <= 0)
                    {
                        command.CommandText = @"
                            DELETE FROM Card
                            WHERE ID = @cardId;
                        ";
                    }
                    else
                    {
                        command.CommandText = @"
                            UPDATE Card
                            SET Count = @newCount
                            WHERE ID = @cardId;
                        ";

                        command.Parameters.AddWithValue(
                            "@newCount",
                            newCount);
                    }

                    command.Parameters.AddWithValue(
                        "@cardId",
                        cardID);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
        
        public static async Task<List<Card>> GetCards(SearchOptions options)
        {
            var cards = new List<Card>();

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    var conditions = new List<string>();

                    /*
                     * KEYWORDS
                     *
                     * Każdy keyword musi występować w danych karty albo
                     * w danych jednej z jej twarzy.
                     */
                    if (options.Keywords != null && options.Keywords.Count > 0)
                    {
                        for (int i = 0; i < options.Keywords.Count; i++)
                        {
                            string parameterName = $"@keyword{i}";

                            conditions.Add($@"
                                (
                                    c.Name LIKE '%' || {parameterName} || '%'
                                    OR c.OracleText LIKE '%' || {parameterName} || '%'
                                    OR EXISTS
                                    (
                                        SELECT 1
                                        FROM CardFace cf
                                        WHERE cf.CardID = c.ID
                                          AND
                                          (
                                              cf.Name LIKE '%' || {parameterName} || '%'
                                              OR cf.OracleText LIKE '%' || {parameterName} || '%'
                                          )
                                    )
                                )
                            ");

                            command.Parameters.AddWithValue(
                                parameterName,
                                options.Keywords[i]);
                        }
                    }

                    /*
                     * TYPES
                     *
                     * Types jest zapisane jako:
                     * Legendary,Creature,Dwarf,Advisor
                     *
                     * Szukamy pełnego elementu, a nie fragmentu słowa.
                     */
                    if (options.Types != null && options.Types.Count > 0)
                    {
                        for (int i = 0; i < options.Types.Count; i++)
                        {
                            string parameterName = $"@type{i}";

                            conditions.Add($@"
                                (
                                    (',' || c.Types || ',') LIKE
                                    '%,' || {parameterName} || ',%'
                                    OR EXISTS
                                    (
                                        SELECT 1
                                        FROM CardFace cf
                                        WHERE cf.CardID = c.ID
                                          AND (',' || cf.Types || ',') LIKE
                                              '%,' || {parameterName} || ',%'
                                    )
                                )
                            ");

                            command.Parameters.AddWithValue(
                                parameterName,
                                options.Types[i]);
                        }
                    }

                    /*
                     * COLORS
                     *
                     * Dopasowanie dotyczy karty albo jednej z jej twarzy.
                     * Dla karty dwustronnej wszystkie wybrane kolory muszą
                     * znajdować się na tej samej twarzy.
                     */
                    if (options.Colors != null && options.Colors.Count > 0)
                    {
                        bool wantsColorless = options.Colors.Contains("{C}");

                        var actualColors = options.Colors
                            .Where(color => color != "{C}")
                            .ToList();
                        var colorParameterNames = new List<string>();

                        for (int i = 0; i < actualColors.Count; i++)
                        {
                            string parameterName = $"@color{i}";
                            colorParameterNames.Add(parameterName);
                            command.Parameters.AddWithValue(
                                parameterName,
                                actualColors[i]);
                        }

                        var cardColorCondition = CreateColorCondition(
                            "c.Colors",
                            colorParameterNames,
                            wantsColorless,
                            options.ColorSearchType);
                        var cardFaceColorCondition = CreateColorCondition(
                            "cf.Colors",
                            colorParameterNames,
                            wantsColorless,
                            options.ColorSearchType);

                        conditions.Add($@"
                            (
                                {cardColorCondition}
                                OR EXISTS
                                (
                                    SELECT 1
                                    FROM CardFace cf
                                    WHERE cf.CardID = c.ID
                                      AND {cardFaceColorCondition}
                                )
                            )
                        ");
                    }

                    /*
                     * COLOR IDENTITY
                     *
                     * ColorIdentity jest zapisane jako CSV, np.:
                     * W
                     * W,U
                     * W,U,B
                     *
                     * {C} oznacza colorless, czyli:
                     * c.ColorIdentity == ''
                     */
                    if (options.ColorIdentity != null &&
                        options.ColorIdentity.Count > 0)
                    {
                        bool wantsColorlessIdentity =
                            options.ColorIdentity.Contains("{C}");

                        var actualIdentityColors = options.ColorIdentity
                            .Where(color => color != "{C}")
                            .ToList();

                        /*
                         * {C} = dokładnie colorless identity.
                         */
                        if (wantsColorlessIdentity)
                        {
                            conditions.Add(@"
                                (
                                    c.ColorIdentity IS NULL
                                    OR c.ColorIdentity = ''
                                )
                            ");
                        }

                        /*
                         * Pozostałe kolory muszą występować
                         * w ColorIdentity.
                         */
                        for (int i = 0; i < actualIdentityColors.Count; i++)
                        {
                            string parameterName = $"@identity{i}";

                            conditions.Add($@"
                                (',' || c.ColorIdentity || ',') LIKE
                                '%,' || {parameterName} || ',%'
                            ");

                            command.Parameters.AddWithValue(
                                parameterName,
                                actualIdentityColors[i]);
                        }
                    }

                    /*
                     * CMC
                     */
                    if (options.UseCmc)
                    {
                        conditions.Add(
                            "c.ConvertedManaCost = @cmc");

                        command.Parameters.AddWithValue(
                            "@cmc",
                            options.Cmc);
                    }

                    /*
                     * QUERY
                     */
                    command.CommandText = $@"
                        SELECT {CardColumns}
                        FROM Card c
                        LEFT JOIN Location l
                            ON c.LocationID = l.ID
                        {(conditions.Count > 0
                            ? "WHERE " + string.Join(
                                "\nAND ",
                                conditions)
                            : "")};
                    ";

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cards.Add(ReadCard(reader));
                        }
                    }
                }
            }

            await PopulateCardFaces(cards);
            return cards;
        }

        public static async Task<List<Card>> GetCards(
            string name,
            string printID = "",
            bool ignorePrintIfEmpty = true)
        {
            var cards = new List<Card>();

            using (var connection = Database.GetConnection())
            {
                await connection.OpenAsync();

                using (var command = connection.CreateCommand())
                {
                    if (!string.IsNullOrEmpty(printID) || !ignorePrintIfEmpty)
                    {
                        command.CommandText = $@"
                    SELECT {CardColumns}
                    FROM Card c
                    LEFT JOIN Location l
                        ON c.LocationID = l.ID
                    WHERE c.Name = @name
                      AND c.PrintID = @printID;
                ";

                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@printID", printID);
                    }
                    else
                    {
                        command.CommandText = $@"
                    SELECT {CardColumns}
                    FROM Card c
                    LEFT JOIN Location l
                        ON c.LocationID = l.ID
                    WHERE c.Name LIKE '%' || @name || '%'
                       OR c.OracleText LIKE '%' || @name || '%'
                       OR (',' || c.Types || ',') LIKE '%,' || @name || ',%';
                ";

                        command.Parameters.AddWithValue("@name", name);
                    }

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            cards.Add(ReadCard(reader));
                        }
                    }
                }
            }

            await PopulateCardFaces(cards);
            return cards;
        }

        public static async Task<List<Card>> SearchCards(
            string input,
            Action<int, int> progress)
        {
            var cards = new List<Card>();

            var ownedCards = await GetCardsDistinct();

            var currentPageCount = 1;

            ScryfallSearch searchResult;

            do
            {
                await Task.Delay(500);

                searchResult = await ScryfallEndpoints.SearchCards(input);

                var totalPages = (int)Math.Ceiling(
                    searchResult.total_cards / 175.0);

                progress?.Invoke(
                    currentPageCount,
                    totalPages);

                currentPageCount++;

                foreach (var scryfallCard in searchResult.data)
                {
                    if (scryfallCard.prices?.eur == null ||
                        scryfallCard.image_uris?.normal == null ||
                        ownedCards.All(e => e.Name != scryfallCard.name))
                    {
                        continue;
                    }

                    var genericCard = scryfallCard.CreateCardObject();

                    var groupedCards = (await GetCards(genericCard.Name))
                        .GroupBy(e => e.PrintID)
                        .Select(e =>
                        {
                            var card = e.First();

                            card.Count = e.Sum(c => c.Count);

                            return card;
                        });

                    cards.AddRange(groupedCards);
                }

                if (searchResult.has_more)
                {
                    input = searchResult.next_page.Substring(
                        searchResult.next_page.IndexOf(
                            "search?",
                            StringComparison.Ordinal) + "search?".Length);
                }

            } while (searchResult.has_more);

            return cards;
        }
    }
}
