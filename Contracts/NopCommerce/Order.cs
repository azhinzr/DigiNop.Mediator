using System;
using System.Collections.Generic;

namespace Contracts.NopCommerce
{
    public class Order
    {
        public int store_id { get; set; }
        public object pick_up_in_store { get; set; }
        public string payment_method_system_name { get; set; }
        public string customer_currency_code { get; set; }
        public double currency_rate { get; set; }
        public int customer_tax_display_type_id { get; set; }
        public object vat_number { get; set; }
        public double order_subtotal_incl_tax { get; set; }
        public double order_subtotal_excl_tax { get; set; }
        public double order_sub_total_discount_incl_tax { get; set; }
        public double order_sub_total_discount_excl_tax { get; set; }
        public double order_shipping_incl_tax { get; set; }
        public double order_shipping_excl_tax { get; set; }
        public double payment_method_additional_fee_incl_tax { get; set; }
        public double payment_method_additional_fee_excl_tax { get; set; }
        public string tax_rates { get; set; }
        public double order_tax { get; set; }
        public double order_discount { get; set; }
        public double order_total { get; set; }
        public double refunded_amount { get; set; }
        public object reward_points_were_added { get; set; }
        public string checkout_attribute_description { get; set; }
        public int customer_language_id { get; set; }
        public int affiliate_id { get; set; }
        public string customer_ip { get; set; }
        public string authorization_transaction_id { get; set; }
        public object authorization_transaction_code { get; set; }
        public string authorization_transaction_result { get; set; }
        public object capture_transaction_id { get; set; }
        public object capture_transaction_result { get; set; }
        public object subscription_transaction_id { get; set; }
        public DateTime? paid_date_utc { get; set; }
        public string shipping_method { get; set; }
        public string shipping_rate_computation_method_system_name { get; set; }
        public string custom_values_xml { get; set; }
        public bool deleted { get; set; }
        public DateTime created_on_utc { get; set; }
        public Customer customer { get; set; }
        public int customer_id { get; set; }
        public BillingAddress billing_address { get; set; }
        public ShippingAddress shipping_address { get; set; }
        public List<OrderItem> order_items { get; set; }
        public string order_status { get; set; }
        public string payment_status { get; set; }
        public string shipping_status { get; set; }
        public string customer_tax_display_type { get; set; }
        public int id { get; set; }
    }

}
