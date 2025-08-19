using ProvaPub.Entities;
using ProvaPub.Extensions;
using ProvaPub.Interfaces;
using ProvaPub.Repository;
using ProvaPub.Results;

namespace ProvaPub.Services
{
    public class ProductService : IProductService
    {
        TestDbContext _ctx;

        public ProductService(TestDbContext ctx)
        {
            _ctx = ctx;
        }

        public PagedResult<Product> ListProducts(int page)
        {
            return _ctx.Products.ToPagedResult(page);
        }
    }
}
