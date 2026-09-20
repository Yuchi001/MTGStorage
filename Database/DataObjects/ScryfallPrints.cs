namespace MTGStorage.Database.DataObjects
{
    public class ScryfallPrints
    {
        public int total_cards { get; set; }
        public PrintData[] data { get; set; }
    }

    public class PrintData
    {
        public string set_name { get; set; }
        public string collector_number { get; set; }
        public ImageUris image_uris { get; set; }
        public Prices prices { get; set; }
    }
}