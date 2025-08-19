using ProvaPub.Entities;
using ProvaPub.Results;

namespace ProvaPub.Interfaces
{
    public interface IProductService
    {
        PagedResult<Product> ListProducts(int page);
    }
}
