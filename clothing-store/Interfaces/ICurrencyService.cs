using static clothing_store.Services.CurrencyService;

namespace clothing_store.Interfaces
{
    public interface ICurrencyService
    {
        Task UpdateCurrencyRatesAsync();
        decimal GetRate(string currencyCode);
    }

}
