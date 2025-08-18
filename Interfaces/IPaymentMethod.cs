namespace ProvaPub.Interfaces
{
    public interface IPaymentMethod
    {
        Task ProcessPayment(decimal amount);
    }
}
