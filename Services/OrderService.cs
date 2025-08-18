using ProvaPub.Entities;
using ProvaPub.Interfaces;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class OrderService : IOrderService
	{
        TestDbContext _ctx;
		private readonly IDictionary<string, IPaymentMethod> _paymentMethods;

        public OrderService(TestDbContext ctx, IEnumerable<IPaymentMethod> paymentMethods)
        {
            _ctx = ctx;
            _paymentMethods = paymentMethods
            .ToDictionary(p => p.GetType().Name.Replace("Payment", "").ToLower());
        }

        public async Task<Order> PayOrder(string paymentMethodKey, decimal paymentValue, int customerId)
        {
            if (!_paymentMethods.TryGetValue(paymentMethodKey.ToLower(), out var paymentMethod))
                throw new ArgumentException("Forma de pagamento inválida");

            await paymentMethod.ProcessPayment(paymentValue);

            var order = new Order
            {
                CustomerId = customerId,
                Value = paymentValue,
                OrderDate = DateTime.UtcNow
            };

            await _ctx.Orders.AddAsync(order);
            await _ctx.SaveChangesAsync();

            order.OrderDate = TimeZoneInfo.ConvertTimeFromUtc(order.OrderDate, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));
            
            return await InsertOrder(order);
        }

		public async Task<Order> InsertOrder(Order order)
        {
			//Insere pedido no banco de dados
			return (await _ctx.Orders.AddAsync(order)).Entity;
        }
	}
}
