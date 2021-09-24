using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.NopCommerce;
  
namespace Mediator.ACL.NopCommerce.services
{
    public interface INopService
    { 
       Task<List<Product>> GetProducts(int pageNo);
        Task<int> GetProductsCount();
      }


}
