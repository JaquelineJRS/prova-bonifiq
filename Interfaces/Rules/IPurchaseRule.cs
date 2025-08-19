using ProvaPub.Entities;

namespace ProvaPub.Interfaces.Rules
{
    public interface IPurchaseRule
    {
        Task<bool> IsSatisfiedAsync(Customer customer, decimal purchaseValue);
    }
}
