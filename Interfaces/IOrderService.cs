using ProvaPub.Entities;

namespace ProvaPub.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId);
    }
}
