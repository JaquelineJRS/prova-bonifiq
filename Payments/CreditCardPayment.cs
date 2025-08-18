using ProvaPub.Interfaces;

namespace ProvaPub.Payments
{
    public class CreditCardPayment : IPaymentMethod
    {
        public Task ProcessPayment(decimal amount)
        {
            return Task.CompletedTask;
        }
    }
}
