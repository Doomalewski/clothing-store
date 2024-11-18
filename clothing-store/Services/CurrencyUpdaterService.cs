using clothing_store.Services;

public class CurrencyUpdaterService : IHostedService
{
    private readonly CurrencyService _currencyService;
    private Timer _timer;

    public CurrencyUpdaterService(CurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromDays(1)); // Ustawienie co 24 godziny
        return Task.CompletedTask;
    }

    private async void DoWork(object state)
    {
        await _currencyService.UpdateCurrencyRatesAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}
