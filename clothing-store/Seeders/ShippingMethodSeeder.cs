using clothing_store.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class ShippingMethodSeeder
{
    private readonly StoreDbContext _context;

    public ShippingMethodSeeder(StoreDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        if (!await _context.ShippingMethods.AnyAsync())
        {
            var shippingMethods = new List<ShippingMethod>
            {
                new ShippingMethod
                {
                    Name = "Standard Shipping",
                    Price = 10.99f,
                    EstimatedDeliveryTime = "3-5 business days"
                },
                new ShippingMethod
                {
                    Name = "Express Shipping",
                    Price = 19.99f,
                    EstimatedDeliveryTime = "1-2 business days"
                },
                new ShippingMethod
                {
                    Name = "Free Shipping",
                    Price = 0.00f,
                    EstimatedDeliveryTime = "5-7 business days"
                }
            };

            await _context.ShippingMethods.AddRangeAsync(shippingMethods);
            await _context.SaveChangesAsync();
        }
    }
}
