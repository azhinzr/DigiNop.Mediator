using System;
using System.Collections.Generic;
using System.Text;
using Contracts.Digikala;

namespace Contracts
{
  public  class DigikalaProducts
    {
        public List<Product> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
