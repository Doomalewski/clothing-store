namespace clothing_store.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<Currency> GetCurrencyByCodeAsync(string currencyCode);
        Task SaveCurrencyAsync(Currency currency);
        Task SaveCurrenciesAsync(IEnumerable<Currency> currencies);
        Task<List<Currency>> GetAllCurrenciesAsync();
    }

}
