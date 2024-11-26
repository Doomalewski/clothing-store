using clothing_store.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

public class CurrencyUpdaterService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private Timer _timer;

    public CurrencyUpdaterService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1)); // Ustawienie co 24 godziny
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        using (var scope = _scopeFactory.CreateScope()) // Tworzymy nowy zakres
        {
            var currencyService = scope.ServiceProvider.GetRequiredService<ICurrencyService>();
            await currencyService.UpdateCurrencyRatesAsync(); // Wywołanie metody w CurrencyService
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}
