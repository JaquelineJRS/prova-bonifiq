using Microsoft.EntityFrameworkCore;
using ProvaPub.Entities;
using ProvaPub.Interfaces.Providers;
using ProvaPub.Interfaces.Rules;
using ProvaPub.Repository;

namespace ProvaPub.Services.Rules
{
    public class SinglePurchasePerMonthRule : IPurchaseRule
    {
        private readonly TestDbContext _ctx;
        private readonly IDateTimeProvider _dateTimeProvider;

        public SinglePurchasePerMonthRule(TestDbContext ctx, IDateTimeProvider dateTimeProvider)
        {
            _ctx = ctx;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<bool> IsSatisfiedAsync(Customer customer, decimal purchaseValue)
        {
            var baseDate = _dateTimeProvider.UtcNow.AddMonths(-1);
            var count = await _ctx.Orders.CountAsync(o => o.CustomerId == customer.Id && o.OrderDate >= baseDate);
            return count == 0;
        }
    }
}
