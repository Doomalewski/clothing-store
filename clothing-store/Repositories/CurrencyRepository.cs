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
        public async Task UpdateCurrenciesAsync(List<CurrencyDto> currencies)
        {
            // Pobranie istniejących walut z bazy danych
            var existingCurrencies = await _context.Currencies
                .Where(c => currencies.Select(cur => cur.Code).Contains(c.Code))
                .ToListAsync();

            foreach (var currency in currencies)
            {
                var existingCurrency = existingCurrencies.FirstOrDefault(c => c.Code == currency.Code);

                if (existingCurrency != null)
                {
                    // Zaktualizowanie istniejącej waluty
                    existingCurrency.Rate = currency.Rate;
                    existingCurrency.LastUpdated = DateTime.UtcNow;
                }
                else
                {
                    // Dodanie nowej waluty
                    var newCurrency = new Currency
                    {
                        Name = currency.Name,
                        Code = currency.Code,
                        Rate = currency.Rate,
                        LastUpdated = DateTime.UtcNow
                    };

                    _context.Currencies.Add(newCurrency);
                }
            }

            // Zapisanie zmian w bazie danych
            await _context.SaveChangesAsync();
        }

        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            return await _context.Currencies.ToListAsync();
        }
    }

}
