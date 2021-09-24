namespace Contracts.Digikala
{
    public class Product
    {
        public int id { get; set; }
        public int category_id { get; set; }
        public string title { get; set; }
        public string image { get; set; }
        public string category_name_fa { get; set; }
        public string category_name_en { get; set; }
        public string moderation_status { get; set; }
    }
}