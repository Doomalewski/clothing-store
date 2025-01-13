using clothing_store.Interfaces;
using clothing_store.Models;

namespace clothing_store.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddOrderAsync(order);
        }
        public async Task<List<ShippingMethod>> GetAllShippingMethodsAsync()
        {
            return await _orderRepository.GetAllShippingMethodsAsync();
        }
        public async Task<List<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await _orderRepository.GetAllPaymentMethodsAsync();
        }
        public async Task<ShippingMethod> GetShippingMethodByIdAsync(int shippingMethodId)
        {
            return await _orderRepository.GetShippingMethodByIdAsync(shippingMethodId);
        }
        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(int paymentMethodId)
        {
            return await _orderRepository.GetPaymentMethodByIdAsync(paymentMethodId);
        }
        public async Task AddOrderProductAsync(OrderProduct orderProduct) => await _orderRepository.AddOrderProductAsync(orderProduct);
        public async Task<List<Order>> GetOrdersByAccountIdAsync(int accountId)
        {
            return await _orderRepository.GetOrdersByAccountIdAsync(accountId);
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }
        public async Task UpdateOrderAsync(Order order) => await _orderRepository.UpdateOrderAsync(order);
        public async Task<List<Order>> GetAllOrdersAsync() => await _orderRepository.GetAllOrdersAsync();
    }
}
