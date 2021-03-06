namespace Contracts.NopCommerce
{
    public class ProductSpecificationAttribute
    {
        public int id { get; set; }
        public int product_id { get; set; }
        public int attribute_type_id { get; set; }
        public int specification_attribute_option_id { get; set; }
        public string custom_value { get; set; }
        public bool allow_filtering { get; set; }
        public bool show_on_product_page { get; set; }
        public int display_order { get; set; }
        public string attribute_type { get; set; }
        public SpecificationAttributeOption specification_attribute_option { get; set; }
    }
}
