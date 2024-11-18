using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_store.Repositories
{
    public class TaxRepository : ITaxRepository
    {
        private readonly StoreDbContext _context;
        public TaxRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task<List<Tax>> GetAllTaxesAsync()
        {
            return await _context.Taxes.ToListAsync();
        }
        public async Task AddTaxAsync(Tax tax)
        {
            await _context.Taxes.AddAsync(tax);
            await _context.SaveChangesAsync();
        }
        public async Task<Tax> GetTaxByIdAsync(int Id)
        {
            return await _context.Taxes.FirstOrDefaultAsync(t => t.TaxId == Id);
        }
    }
}
