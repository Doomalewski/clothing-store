using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace clothing_store.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _context;
        public OrderRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task AddOrderAsync(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order), "Order cannot be null.");
            }

            // Dodaj zamówienie do kontekstu
            await _context.Orders.AddAsync(order);

            // Zapisz zmiany w bazie danych
            await _context.SaveChangesAsync();
        }
        public async Task<List<ShippingMethod>> GetAllShippingMethodsAsync()
        {
            return await _context.ShippingMethods.ToListAsync();
        }
        public async Task<List<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await _context.PaymentMethods.ToListAsync();

        }
        public async Task<PaymentMethod> GetPaymentMethodByIdAsync(int paymentMethodId)
        {
            return await _context.PaymentMethods.FirstOrDefaultAsync(m => m.PaymentMethodId == paymentMethodId);
        }
        public async Task<ShippingMethod> GetShippingMethodByIdAsync(int shippingMethodId)
        {
            return await _context.ShippingMethods.FirstOrDefaultAsync(m => m.ShippingMethodId == shippingMethodId);
        }
    }
}
