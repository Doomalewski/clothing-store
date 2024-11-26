using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.Json;

namespace clothing_store.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private static readonly HttpClient _httpClient = new HttpClient();

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            _currencyRepository = currencyRepository;
        }

        public async Task UpdateCurrencyRatesAsync()
        {
            try
            {
                var response = await _httpClient.GetStringAsync("https://api.nbp.pl/api/exchangerates/tables/A?format=json");
                Console.WriteLine(response);
                if (string.IsNullOrEmpty(response))
                {
                    throw new InvalidOperationException("API response is empty or null.");
                }

                // Deserializacja odpowiedzi JSON
                var rates = JsonConvert.DeserializeObject<List<NbpExchangeRateTable>>(response);

                if (rates == null || rates.Count == 0)
                {
                    throw new InvalidOperationException("No exchange rate data found.");
                }

                // Filtrowanie tylko interesujących walut
                var filteredRates = rates[0].Rates?.Where(rate =>
                    new[] { "USD", "EUR", "GBP", "JPY" }.Contains(rate.Code)).ToList();

                if (filteredRates == null || filteredRates.Count == 0)
                {
                    throw new InvalidOperationException("No relevant exchange rates found.");
                }

                var currencies = MapToCurrencies(filteredRates);

                // Zapisz wybrane kursy walut do bazy danych
                await _currencyRepository.UpdateCurrenciesAsync(currencies);
            }
            catch (Exception ex)
            {
                // Logowanie lub obsługa błędów
                Console.WriteLine($"Error updating currency rates: {ex.Message}");
                throw;  // Zgłoś błąd dalej, aby móc go przechwycić w innych częściach aplikacji, jeśli potrzebujesz
            }
        }

        // Metoda mapująca dane z API na DTO
        public List<CurrencyDto> MapToCurrencies(IEnumerable<Rate> rates)
        {
            return rates.Select(rate => new CurrencyDto
            {
                Name = rate.Currency,
                Code = rate.Code,
                Rate = rate.Mid
            }).ToList();
        }

        // Pobieranie kursu dla wybranej waluty z repozytorium
        public async Task<decimal> GetRateAsync(string currencyCode)
        {
            var currency = await _currencyRepository.GetCurrencyByCodeAsync(currencyCode);
            return currency?.Rate ?? 1; // Domyślnie PLN
        }

        // Pobieranie wszystkich walut z repozytorium
        public async Task<List<Currency>> GetAllCurrenciesAsync()
        {
            return await _currencyRepository.GetAllCurrenciesAsync();
        }
    }

    // Klasa reprezentująca odpowiedź z API
    public class NbpExchangeRateTable
    {
        public string Table { get; set; }
        public string No { get; set; }
        public string EffectiveDate { get; set; }
        public List<Rate> Rates { get; set; }  // Lista walut
    }

    // Klasa reprezentująca szczegóły kursu waluty
    public class Rate
    {
        public string Currency { get; set; } // np. "bat (Tajlandia)"
        public string Code { get; set; }     // np. "THB"
        public decimal Mid { get; set; }     // np. 0.1184
    }
}
