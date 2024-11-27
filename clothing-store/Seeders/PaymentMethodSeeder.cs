using clothing_store.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class PaymentMethodSeeder
{
    private readonly StoreDbContext _context;

    public PaymentMethodSeeder(StoreDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (!await _context.PaymentMethods.AnyAsync())
        {
            var paymentMethods = new List<PaymentMethod>
            {
                new PaymentMethod
                {
                    Name = "Credit Card"
                },
                new PaymentMethod
                {
                    Name = "PayPal"
                },
                new PaymentMethod
                {
                    Name = "Bank Transfer"
                }
            };

            await _context.PaymentMethods.AddRangeAsync(paymentMethods);
            await _context.SaveChangesAsync();
        }
    }
}
