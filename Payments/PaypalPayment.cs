using ProvaPub.Interfaces;

namespace ProvaPub.Payments
{
    public class PaypalPayment : IPaymentMethod
    {
        public Task ProcessPayment(decimal amount)
        {
            return Task.CompletedTask;
        }
    }
}
