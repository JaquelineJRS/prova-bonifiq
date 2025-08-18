using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProvaPub.Models;
using ProvaPub.Repository;
using System;

namespace ProvaPub.Services
{
	public class RandomService
	{
		int seed;
        TestDbContext _ctx;
		public RandomService()
        {
            var contextOptions = new DbContextOptionsBuilder<TestDbContext>()
    .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Teste;Trusted_Connection=True;")
    .Options;
            seed = Guid.NewGuid().GetHashCode();

            _ctx = new TestDbContext(contextOptions);
        }
        public async Task<int> GetRandom()
		{
            int number;
            bool saved = false;

            do
            {
                number = new Random(seed).Next(100);
                try
                {
                    _ctx.Numbers.Add(new RandomNumber { Number = number });
                    await _ctx.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 2601)
                {                    
                    _ctx.ChangeTracker.Clear();
                }
            } while (!saved);

            return number;
        }
	}
}
