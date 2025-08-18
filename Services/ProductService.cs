using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public ProductList  ListProducts(int page)
		{
			var pag = new ProductList() {  HasNext=false, TotalCount = 10, Products = _ctx.Products.ToList() };
            var result = pag.Products.Skip((page - 1) * pag.TotalCount)
                .Take(pag.TotalCount)
                .ToList();

            return new ProductList() { HasNext = false, TotalCount = 10, Products = result };
		}
	}
}
