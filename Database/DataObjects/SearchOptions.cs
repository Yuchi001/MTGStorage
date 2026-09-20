using System.Collections.Generic;

namespace MTGStorage.Database.DataObjects
{
    public class SearchOptions
    {
        public List<string> Keywords;
        public List<string> Types;
        public List<string> Colors;
        public EColorsSearchType ColorSearchType;
        public List<string> ColorIdentity;
        public bool UseCmc;
        public decimal Cmc;

        public enum EColorsSearchType
        {
            EXACT = 0,
            AT_LEAST = 1,
            ANY = 2,
        }
    }
}