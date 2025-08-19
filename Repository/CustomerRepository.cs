using Microsoft.EntityFrameworkCore;
using ProvaPub.Entities;
using ProvaPub.Extensions;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Results;

namespace ProvaPub.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TestDbContext _ctx;

        public CustomerRepository(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Customer?> GetByIdWithOrdersAsync(int customerId)
        {
            return await _ctx.Customers
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(c => c.Id == customerId);
        }

        public PagedResult<Customer> GetPaged(int page)
        {
            return _ctx.Customers.ToPagedResult(page);
        }
    }
}
