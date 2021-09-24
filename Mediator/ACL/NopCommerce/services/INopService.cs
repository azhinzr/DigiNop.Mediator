using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.NopCommerce;
using Contracts.NopCommerce.updateProduct;
 
namespace Mediator.ACL.NopCommerce.services
{
    public interface INopService
    { 
       Task<List<Product>> GetProducts(int pageNo);
        Task<int> GetProductsCount();
        Task UpdateProduct(UpdateProductDto product);
     }


}
