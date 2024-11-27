using clothing_store.Interfaces;

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
    }
}
