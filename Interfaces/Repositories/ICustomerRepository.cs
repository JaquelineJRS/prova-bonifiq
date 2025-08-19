using ProvaPub.Entities;
using ProvaPub.Results;

namespace ProvaPub.Interfaces.Repositories
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdWithOrdersAsync(int customerId);
        PagedResult<Customer> GetPaged(int page);
    }
}
