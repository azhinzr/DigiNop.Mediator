namespace Contracts.NopCommerce
{
    public class SpecificationAttributeOption
    {
        public int id { get; set; }
        public int specification_attribute_id { get; set; }
        public string name { get; set; }
        public object color_squares_rgb { get; set; }
        public int display_order { get; set; }
    }
}
