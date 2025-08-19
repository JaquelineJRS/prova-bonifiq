using ProvaPub.Entities;
using ProvaPub.Interfaces.Providers;
using ProvaPub.Interfaces.Rules;

namespace ProvaPub.Services.Rules
{
    public class BusinessHoursRule : IPurchaseRule
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public BusinessHoursRule(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public Task<bool> IsSatisfiedAsync(Customer customer, decimal purchaseValue)
        {
            var now = _dateTimeProvider.UtcNow;
            bool isWeekday = now.DayOfWeek != DayOfWeek.Saturday && now.DayOfWeek != DayOfWeek.Sunday;
            bool isBusinessHor = now.Hour >= 8 && now.Hour <= 18;

            return Task.FromResult(isWeekday && isBusinessHor);
        }
    }
}
