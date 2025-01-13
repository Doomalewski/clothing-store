using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(Order order);
        Task<List<ShippingMethod>> GetAllShippingMethodsAsync();
        Task<List<PaymentMethod>> GetAllPaymentMethodsAsync();
        Task<ShippingMethod> GetShippingMethodByIdAsync(int shippingMethodId);
        Task<PaymentMethod> GetPaymentMethodByIdAsync(int paymentMethodId);
        Task AddOrderProductAsync(OrderProduct orderProduct);
        Task<List<Order>> GetOrdersByAccountIdAsync(int accountId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderAsync(Order order);
        Task<List<Order>>GetAllOrdersAsync();
    }

}
