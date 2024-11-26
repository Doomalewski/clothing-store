using clothing_store.Models;
using System;
using System.Collections.Generic;
using System.Linq;

public class CurrencySeeder
{
    private readonly StoreDbContext _context;

    public CurrencySeeder(StoreDbContext context)
    {
        _context = context;
    }

    public void Seed()
    {
        if (!_context.Currencies.Any())
        {
            var currencies = new List<Currency>
            {
                new Currency
                {
                    Name = "Polski Złoty",
                    Code = "PLN",
                    Rate = 1.0m, // PLN to baza, więc kurs wynosi 1
                    LastUpdated = DateTime.UtcNow
                },
                new Currency
                {
                    Name = "Dolar Amerykański",
                    Code = "USD",
                    Rate = 4.2m, // Przykładowy kurs do PLN
                    LastUpdated = DateTime.UtcNow
                },
                new Currency
                {
                    Name = "Euro",
                    Code = "EUR",
                    Rate = 4.6m, // Przykładowy kurs do PLN
                    LastUpdated = DateTime.UtcNow
                },
                new Currency
                {
                    Name = "Funt Brytyjski",
                    Code = "GBP",
                    Rate = 5.2m, // Przykładowy kurs do PLN
                    LastUpdated = DateTime.UtcNow
                },
                new Currency
                {
                    Name = "Jen Japoński",
                    Code = "JPY",
                    Rate = 0.032m, // Przykładowy kurs do PLN
                    LastUpdated = DateTime.UtcNow
                }
            };

            _context.Currencies.AddRange(currencies);
            _context.SaveChanges();
        }
    }
}
