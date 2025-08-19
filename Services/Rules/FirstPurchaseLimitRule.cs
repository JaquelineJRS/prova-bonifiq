using ProvaPub.Entities;
using ProvaPub.Interfaces.Rules;

namespace ProvaPub.Services.Rules
{
    public class FirstPurchaseLimitRule : IPurchaseRule
    {
        public Task<bool> IsSatisfiedAsync(Customer customer, decimal purchaseValue)
        {
            var haveBoughtBefore = customer.Orders.Any();
            if (!haveBoughtBefore && purchaseValue > 100)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
