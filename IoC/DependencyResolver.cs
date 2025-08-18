using ProvaPub.Interfaces;
using ProvaPub.Payments;
using ProvaPub.Services;

namespace ProvaPub.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();

            services.AddScoped<RandomService>();

            services.AddScoped<IPaymentMethod, PixPayment>();
            services.AddScoped<IPaymentMethod, CreditCardPayment>();
            services.AddScoped<IPaymentMethod, PaypalPayment>();
        }
    }
}
