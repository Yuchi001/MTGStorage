using System.Collections.Generic;

namespace MTGStorage.Database.DataObjects
{
    public class CardFace
    {
        public int ID { get; set; }
        public int CardID { get; set; }
        public string Name { get; set; }
        public string OracleText { get; set; }
        public string ImageUrl { get; set; }
        public string Colors { get; set; }
        public string ManaCost { get; set; }
        public List<string> Types { get; set; }

        public Card Card { get; set; }
    }
}