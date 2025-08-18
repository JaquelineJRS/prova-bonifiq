using ProvaPub.Entities;
using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface ICustomerService
    {
        public PagedResult<Customer> ListCustomers(int page);
        Task<bool> CanPurchase(int customerId, decimal purchaseValue);
    }
}
