using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_store.Repositories
{
    public class BrandRepository : IBrandRepository
    {
        private readonly StoreDbContext _context;
        public BrandRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<Brand> GetBrandByIdAsync(int Id)
        {
            return await _context.Brands.FirstOrDefaultAsync(b => b.BrandId == Id);
        }
        public async Task<List<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands.ToListAsync();
        }
    }
}
