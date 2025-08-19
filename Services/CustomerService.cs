using ProvaPub.Entities;
using ProvaPub.Interfaces;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Rules;
using ProvaPub.Results;

namespace ProvaPub.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEnumerable<IPurchaseRule> _rules;

        public CustomerService(ICustomerRepository customerRepository, IEnumerable<IPurchaseRule> rules)
        {
            _customerRepository = customerRepository;
            _rules = rules;
        }

        public PagedResult<Customer> ListCustomers(int page)
        {
            return _customerRepository.GetPaged(page);
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0)
                throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            var customer = await _customerRepository.GetByIdWithOrdersAsync(customerId);
            if (customer == null)
                throw new InvalidOperationException($"Customer Id {customerId} does not exist");

            foreach (var rule in _rules)
            {
                if (!await rule.IsSatisfiedAsync(customer, purchaseValue))
                    return false;
            }

            return true;
        }
    }
}
