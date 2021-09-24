using System.Collections.Generic;

namespace Contracts.NopCommerce
{
    public class Attribute
    {
        public int product_attribute_id { get; set; }
        public string product_attribute_name { get; set; }
        public string text_prompt { get; set; }
        public bool is_required { get; set; }
        public int attribute_control_type_id { get; set; }
        public int display_order { get; set; }
        public object default_value { get; set; }
        public string attribute_control_type_name { get; set; }
        public List<AttributeValue> attribute_values { get; set; }
        public int id { get; set; }
    }
}
