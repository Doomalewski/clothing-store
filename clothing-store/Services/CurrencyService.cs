using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.EntityFrameworkCore;
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

        // Pobranie kursów walut z API NBP i zapisanie w bazie danych
        public async Task UpdateCurrencyRatesAsync()
        {
            var response = await _httpClient.GetStringAsync("https://api.nbp.pl/api/exchangerates/tables/A?format=json");
            var rates = JsonSerializer.Deserialize<List<NbpExchangeRateTable>>(response);

            if (rates != null && rates.Count > 0)
            {
                var currencies = MapToCurrencies(rates[0].Rates);

                // Zapisz wszystkie kursy walut do bazy danych
                await _currencyRepository.SaveCurrenciesAsync(currencies);
            }
        }

        // Mapowanie danych z API na naszą reprezentację waluty
        private List<Currency> MapToCurrencies(List<NbpRate> nbpRates)
        {
            var currencies = new List<Currency>();

            foreach (var rate in nbpRates)
            {
                currencies.Add(new Currency
                {
                    Name = rate.Currency,
                    Code = rate.Code,
                    Rate = rate.Mid // Kurs średni
                });
            }

            return currencies;
        }

        // Pobieranie kursu dla wybranej waluty z repozytorium
        public decimal GetRate(string currencyCode)
        {
            var currency = _currencyRepository.GetCurrencyByCodeAsync(currencyCode).Result;
            return currency?.Rate ?? 1; // Domyślnie PLN
        }
    }
        public class NbpExchangeRateTable
        {
            public string No { get; set; } // Numer tabeli, np. "A123/2024"
            public DateTime EffectiveDate { get; set; } // Data, na którą kursy są ważne
            public List<NbpRate> Rates { get; set; } // Lista kursów walut
        }

        public class NbpRate
        {
            public string Currency { get; set; } // Nazwa waluty, np. "dolar amerykański"
            public string Code { get; set; } // Kod waluty, np. "USD"
            public decimal Mid { get; set; } // Kurs średni waluty
        }
    }