using clothing_store.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace clothing_store.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;
        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost("update")]
        public async Task<IActionResult> UpdateCurrencies()
        {
            await _currencyService.UpdateCurrencyRatesAsync();
            return Ok("Currency rates updated successfully.");
        }
    }
}
