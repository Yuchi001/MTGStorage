using System.Globalization;
using System.Linq;

namespace MTGStorage.Database.DataObjects
{
    public static class Extensions
    {
        public static Card CreateCardObject(this ScryfallCard card, string printID = "") => new Card
        {
            Name = card.name,
            ImageUrl = card.image_uris.normal,
            PrintID = printID,
            Price = decimal.Parse(
                card.prices.eur,
                CultureInfo.InvariantCulture),
            Rank = card.edhrec_rank,
            OracleText = card.oracle_text ?? "",
            Colors = card.colors == null
                ? ""
                : string.Join(",", card.colors),
            ColorIdentity = card.color_identity == null
                ? ""
                : string.Join(",", card.color_identity),
            ManaCost = card.mana_cost ?? "",
            ConvertedManaCost = (int)card.cmc,
            Types = card.type_line
                .Replace("-", "")
                .Replace("/", "")
                .Replace("—", "")
                .Replace("  ", " ")
                .Split(' ')
                .ToList(),
            CardFaces = card.card_faces?.Count > 0 ? card.card_faces.Select(e => new CardFace
            {
                Name = e.name,
                OracleText = e.oracle_text,
                ImageUrl = e.image_uris?.normal ?? "",
                Colors = e.colors == null
                    ? ""
                    : string.Join(",", e.colors),
                ManaCost = e.mana_cost ?? "",
                Types = e.type_line.Replace("-",
                        "")
                    .Replace("—",
                        "")
                    .Replace("  ",
                        " ")
                    .Split(' ')
                    .ToList(),
            }).ToList() : null,
        };
    }
}
