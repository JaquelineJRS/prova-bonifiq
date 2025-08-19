using ProvaPub.Interfaces;
using ProvaPub.Interfaces.Providers;
using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Rules;
using ProvaPub.Payments;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Services.Providers;
using ProvaPub.Services.Rules;

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

            services.AddScoped<IPurchaseRule, FirstPurchaseLimitRule>();
            services.AddScoped<IPurchaseRule, SinglePurchasePerMonthRule>();
            services.AddScoped<IPurchaseRule, BusinessHoursRule>();

            services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

            services.AddScoped<ICustomerRepository, CustomerRepository>();
        }
    }
}
