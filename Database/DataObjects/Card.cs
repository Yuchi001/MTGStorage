using System.Collections.Generic;

namespace MTGStorage.Database.DataObjects
{
    public class Card
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PrintID { get; set; }
        public string ImageUrl { get; set; }
        public int Count { get; set; }
        public int LocationID { get; set; }
        public decimal Price { get; set; }
        public int Rank { get; set; }
        public string OracleText { get; set; }
        public string Colors { get; set; }
        public string ColorIdentity { get; set; }
        public string ManaCost { get; set; }
        public int ConvertedManaCost { get; set; }
        public List<string> Types { get; set; }
        public Location Location { get; set; }
        public List<CardFace> CardFaces { get; set; }
    }
}