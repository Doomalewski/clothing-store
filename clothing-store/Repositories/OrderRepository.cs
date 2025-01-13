using clothing_store.Interfaces;
using clothing_store.Models;
using EllipticCurve.Utils;
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
        public async Task AddOrderProductAsync(OrderProduct orderProduct)
        {
            if (orderProduct == null)
            {
                throw new ArgumentNullException(nameof(orderProduct), "Order cannot be null.");
            }

            // Dodaj zamówienie do kontekstu
            await _context.OrderProducts.AddAsync(orderProduct);

            // Zapisz zmiany w bazie danych
            await _context.SaveChangesAsync();
        }
        public async Task<List<Order>> GetOrdersByAccountIdAsync(int accountId)
        {
            return await _context.Orders
                .Include(o => o.Products)
                .ThenInclude(op => op.Product)
                .Include(s => s.Shipping)
                .Include(p=>p.Payment)
                .Where(o => o.AccountId == accountId)
                .ToListAsync();
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Products)
                .ThenInclude(op => op.Product)
                .Include(s => s.Shipping)
                .Include(p => p.Payment)
                .Where(o => o.OrderId == orderId)
                .FirstOrDefaultAsync();
        }
        public async Task UpdateOrderAsync(Order order)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Products) // Jeśli chcesz załadować produkty związane z zamówieniem
                .FirstOrDefaultAsync(o => o.OrderId == order.OrderId);

            if (existingOrder == null)
            {
                throw new KeyNotFoundException("Zamówienie nie zostało znalezione.");
            }

            // Aktualizacja właściwości
            existingOrder.OrderStatus = order.OrderStatus;
            existingOrder.FullPrice = order.FullPrice;  // Jeśli cena została zmieniona
            existingOrder.Shipping = order.Shipping;    // Zaktualizowanie metody wysyłki
            existingOrder.Payment = order.Payment;      // Zaktualizowanie metody płatności
            existingOrder.Street = order.Street;        // Zaktualizowanie adresu
            existingOrder.City = order.City;
            existingOrder.State = order.State;
            existingOrder.ZipCode = order.ZipCode;
            existingOrder.Country = order.Country;
            existingOrder.Date = order.Date;  // Zaktualizowanie daty zamówienia

            // Zapisanie zmian do bazy danych
            _context.Orders.Update(existingOrder);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Products)
                .ThenInclude(op => op.Product)
                .Include(s => s.Shipping)
                .Include(p => p.Payment)
                .ToListAsync();
        }

    }
}
