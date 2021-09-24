using Contracts;
using Contracts.Digikala;
using Contracts.Digikala.updates;
using System.Threading.Tasks;

namespace Mediator.ACL.Digikala.services
{
    public interface IDigikalaService
    {
         Task<Item> GetProductbyId(string id);
        Task UpdateVariant(UpdateProductDto productDto, int id);
    }


}
