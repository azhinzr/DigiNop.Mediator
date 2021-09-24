namespace Contracts.NopCommerce
{
    public class ProductAttributeCombination
    {
        public int product_id { get; set; }
        public string attributes_xml { get; set; }
        public int stock_quantity { get; set; }
        public string sku { get; set; }
        public string manufacturer_part_number { get; set; }
        public object gtin { get; set; }
        public double? overridden_price { get; set; }
        public int picture_id { get; set; }
        public int id { get; set; }
    }
}
