using System.Collections.Generic;

namespace Contracts.NopCommerce
{
    public class OrderItem
    {
        public List<ProductAttribute> product_attributes { get; set; }
        public int quantity { get; set; }
        public double unit_price_incl_tax { get; set; }
        public double unit_price_excl_tax { get; set; }
        public double price_incl_tax { get; set; }
        public double price_excl_tax { get; set; }
        public double discount_amount_incl_tax { get; set; }
        public double discount_amount_excl_tax { get; set; }
        public double original_product_cost { get; set; }
        public string attribute_description { get; set; }
        public int download_count { get; set; }
        public bool isDownload_activated { get; set; }
        public int license_download_id { get; set; }
        public double item_weight { get; set; }
        public object rental_start_date_utc { get; set; }
        public object rental_end_date_utc { get; set; }
        public Product product { get; set; }
        public int product_id { get; set; }
        public int id { get; set; }
    }
    public class ProductAttribute
    {
        public string value { get; set; }
        public int id { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as ProductAttribute;

            if (item.id == id && item.value.Equals(value))
            {
                return true;
            }

            return false;
        }
    }
}
