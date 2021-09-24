using System;
using System.Collections.Generic;

namespace Contracts.NopCommerce
{
        public class Customer
        {
            public string customer_guid { get; set; }
            public string username { get; set; }
            public string email { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string language_id { get; set; }
            public DateTime? date_of_birth { get; set; }
            public string gender { get; set; }
            public object admin_comment { get; set; }
            public bool is_tax_exempt { get; set; }
            public bool has_shopping_cart_items { get; set; }
            public bool active { get; set; }
            public bool deleted { get; set; }
            public bool is_system_account { get; set; }
            public object system_name { get; set; }
            public string last_ip_address { get; set; }
            public DateTime created_on_utc { get; set; }
            public DateTime? last_login_date_utc { get; set; }
            public DateTime last_activity_date_utc { get; set; }
            public int registered_in_store_id { get; set; }
            public bool subscribed_to_newsletter { get; set; }
            public List<int> role_ids { get; set; }
            public int id { get; set; }
        }
     
}