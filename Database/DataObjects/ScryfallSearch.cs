using System.Collections.Generic;

namespace MTGStorage.Database.DataObjects
{
    public class ScryfallSearch
    {
        public int total_cards { get; set; }
        public bool has_more { get; set; }
        public string next_page { get; set; }
        public List<ScryfallCard> data { get; set; }
    }
}