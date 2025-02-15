﻿using clothing_store.Interfaces;
using clothing_store.Models;
using clothing_store.Services;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace clothing_store.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDbContext _context;
        public ProductRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Product?> GetProductByIdAsync(int productId)
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
        public async Task AddProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p=>p.Brand)
                .Include(t=>t.Tax)
                .ToListAsync();
        }
        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        
        public async Task<List<Product>> GetAllNewProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(t => t.Tax)
                .Where(p =>p.New==true)
                .ToListAsync();
        }
        public async Task<List<Product>> GetAllOldProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Brand)
                .Include(t => t.Tax)
                .Where(p => p.New == false)
                .ToListAsync();
        }
    }
}
