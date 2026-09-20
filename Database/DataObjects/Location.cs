using System.Collections.Generic;

namespace MTGStorage.Database.DataObjects
{
    public class Location
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public int Capacity { get; set; }

        public decimal MinPrice { get; set; }
        public List<Card> Cards { get; set; } = new List<Card>();
    }
}