using System.Collections.Generic;

namespace MTGStorage.Database.DataObjects
{
    public class ScryfallCard
    {
        public string name { get; set; }
        public ImageUris image_uris { get; set; }
        public string prints_search_uri { get; set; }
        public Prices prices { get; set; }
        public int edhrec_rank { get; set; }
        public string oracle_text { get; set; }
        public List<string> colors { get; set; }
        public List<string> color_identity { get; set; }
        public string mana_cost { get; set; }
        public decimal cmc { get; set; }
        public string type_line { get; set; }
        public List<CardFaces> card_faces { get; set; }
    }

    public class CardFaces
    {
        public string name { get; set; }
        public string oracle_text { get; set; }
        public string type_line { get; set; }
        public string mana_cost { get; set; }
        public List<string> colors { get; set; }
        public ImageUris image_uris { get; set; }
    }
    
    public class ImageUris
    {
        public string normal { get; set; }
    }
    
    public class Prices
    {
        public string eur { get; set; }
    }
}
