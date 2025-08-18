using ProvaPub.Interfaces;

namespace ProvaPub.Payments
{
    public class PixPayment : IPaymentMethod
    {
        public Task ProcessPayment(decimal amount)
        {
            return Task.CompletedTask;
        }
    }
}
