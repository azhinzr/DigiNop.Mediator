using Contracts.Digikala;
using System.Collections.Generic;
 
namespace ApiClient.Digikala
{
    public class Data
    {
        public SortData sort_data { get; set; }
        public Pager pager { get; set; }
        public List<object> form_data { get; set; }
        public List<Item> items { get; set; }
        public List<object> meta_data { get; set; }
        public string package_height { get; set; }
        public string package_length { get; set; }
        public string package_width { get; set; }
        public string package_weight { get; set; }
        public string price { get; set; }
        public string seller_physical_stock { get; set; }
        
    }

    
}