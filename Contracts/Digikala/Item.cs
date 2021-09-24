namespace Contracts.Digikala
{
    public class Item
    {
        public int id { get; set; }
        public int seller_id { get; set; }
        public string site { get; set; }
        public bool is_active { get; set; }
        public bool is_archived { get; set; }
        public string title { get; set; }
        public Product product { get; set; }
        public string shipping_type { get; set; }
        public object supplier_code { get; set; }
        public int dk_lead_time { get; set; }
        public int sbs_lead_time { get; set; }
        public Stock stock { get; set; }
        public Price price { get; set; }
        public int? consignment_cap { get; set; }
        public int fulfilment_and_delivery_cost { get; set; }
    }
}