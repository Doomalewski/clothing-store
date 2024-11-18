using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.EntityFrameworkCore;

namespace clothing_store.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly StoreDbContext _context;

        public CurrencyRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task<Currency> GetCurrencyByCodeAsync(string currencyCode)
        {
            return await _context.Currencies.FirstOrDefaultAsync(c => c.Code == currencyCode);
        }

        public async Task SaveCurrencyAsync(Currency currency)
        {
            _context.Currencies.Add(currency);
            await _context.SaveChangesAsync();
        }

        public async Task SaveCurrenciesAsync(IEnumerable<Currency> currencies)
        {
            _context.Currencies.AddRange(currencies);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            return await _context.Currencies.ToListAsync();
        }
    }

}
