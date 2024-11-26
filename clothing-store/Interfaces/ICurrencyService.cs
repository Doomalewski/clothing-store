using static clothing_store.Services.CurrencyService;

namespace clothing_store.Interfaces
{
    public interface ICurrencyService
    {
        Task UpdateCurrencyRatesAsync();
        Task<decimal> GetRateAsync(string currencyCode);
        Task<List<Currency>> GetAllCurrenciesAsync();
    }

}
