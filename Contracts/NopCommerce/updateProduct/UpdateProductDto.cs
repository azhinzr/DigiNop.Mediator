using System;
using System.Collections.Generic;

namespace Contracts.NopCommerce.updateProduct
{
  public  class UpdateProductDto
    {
        public UpdateProductDto()
        {
            product_attribute_combinations=new List<ProductAttributeCombination>();
        } 
        public bool Published { get; set; }
        public int Id { get; set; }
        public int manage_inventory_method_id { get; set; }
        public double price { get; set; }
        public DateTime updated_on_utc { get; set; }

        public List<ProductAttributeCombination> product_attribute_combinations { get; set; }
    }
}
