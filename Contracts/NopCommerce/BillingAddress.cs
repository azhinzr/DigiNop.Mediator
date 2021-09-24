using System;

namespace Contracts.NopCommerce
{
    public class BillingAddress
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public object country_id { get; set; }
        public object country { get; set; }
        public object state_province_id { get; set; }
        public string city { get; set; }
        public string address1 { get; set; }
        public object address2 { get; set; }
        public string zip_postal_code { get; set; }
        public string phone_number { get; set; }
        public string fax_number { get; set; }
        public string customer_attributes { get; set; }
        public DateTime created_on_utc { get; set; }
        public object province { get; set; }
        public int id { get; set; }
    }
}
