﻿using Microsoft.EntityFrameworkCore;

namespace clothing_store.Models.Product
{
    public class ProductRepository
    {
        private readonly StoreDbContext _context;
        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Product?> GetByIdAsync(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("Id has to be a positive number.", nameof(productId));
            }

            try
            {
                var product = await _context.Products
                    .Include(p => p.Brand)
                    .Include(p => p.Opinions)
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                if (product == null)
                {
                    return null;
                }

                return product;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error while loading product: {productId}: {ex.Message}");
                throw new Exception("There was an error while loading product. Contact the admin.");
            }
        }
    }
}
