namespace clothing_store.Interfaces
{
    public interface IOrderService
    {
        Task AddOrderAsync(Order order);
        Task<List<ShippingMethod>> GetAllShippingMethodsAsync();
        Task<List<PaymentMethod>> GetAllPaymentMethodsAsync();
        Task<ShippingMethod> GetShippingMethodByIdAsync(int shippingMethodId);
        Task<PaymentMethod> GetPaymentMethodByIdAsync(int paymentMethodId);

    }

}
