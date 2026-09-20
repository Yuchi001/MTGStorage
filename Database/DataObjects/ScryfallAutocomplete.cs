using System.Collections.Generic;

namespace MTGStorage.Database.DataObjects
{
    public class ScryfallAutocomplete
    {
        public int TotalValues { get; set; }
        public List<string> Data { get; set; }
    }
}