namespace clothing_store.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<List<ShippingMethod>> GetAllShippingMethodsAsync();
        Task<List<PaymentMethod>> GetAllPaymentMethodsAsync();
        Task<PaymentMethod> GetPaymentMethodByIdAsync(int paymentMethodId);
        Task<ShippingMethod> GetShippingMethodByIdAsync(int shippingMethodId);
    }
}
