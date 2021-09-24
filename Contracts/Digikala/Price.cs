using System.Collections.Generic;

namespace Contracts.Digikala
{
    public class Price
    {
        public int id { get; set; }
        public int selling_price { get; set; }
        public int rrp_price { get; set; }
        public int discount { get; set; }
        public int order_limit { get; set; }
        public bool is_promotion_price { get; set; }
        public List<string> tags { get; set; }
        public string start_at { get; set; }
        public string end_at { get; set; }
    }
}