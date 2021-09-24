namespace Contracts.NopCommerce
{
    public class Image
    {
        public int id { get; set; }
        public int picture_id { get; set; }
        public int position { get; set; }
        public string src { get; set; }
        public object attachment { get; set; }
    }
}
