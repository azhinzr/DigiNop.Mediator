namespace Contracts.NopCommerce
{
    public class AttributeValue
    {
        public int type_id { get; set; }
        public int associated_product_id { get; set; }
        public string name { get; set; }
        public string color_squares_rgb { get; set; }
        public object image_squares_image { get; set; }
        public double price_adjustment { get; set; }
        public double weight_adjustment { get; set; }
        public double cost { get; set; }
        public int quantity { get; set; }
        public bool is_pre_selected { get; set; }
        public int display_order { get; set; }
        public object product_image_id { get; set; }
        public string type { get; set; }
        public int id { get; set; }
    }
}
