using clothing_store.Interfaces;
using clothing_store.Models;
using System.Runtime.InteropServices;

namespace clothing_store.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly StoreDbContext _context;
        public DiscountRepository(StoreDbContext context)
        {
            _context = context;
        }
        public async Task CreateSpecialDiscountAsync(SpecialDiscount specialDiscount)
        {
            _context.SpecialDiscounts.Add(specialDiscount);
            await _context.SaveChangesAsync();
        }
    }
}
