using clothing_store.Interfaces;
using clothing_store.Models;
using clothing_store.Services;
using clothing_store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace clothing_store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICurrencyService _currencyService;
        public HomeController(ILogger<HomeController> logger,IProductService productService, ICurrencyService currencyService)
        {
            _logger = logger;
            _productService = productService;
            _currencyService = currencyService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            var currencies = await _currencyService.GetAllCurrenciesAsync();

            // Pobierz wybran¹ walutê z ciasteczek lub domyœlnie ustaw PLN
            var preferredCurrencyCode = Request.Cookies["PreferredCurrency"] ?? "PLN";

            var preferredCurrency = currencies.FirstOrDefault(c => c.Code == preferredCurrencyCode);
            if (preferredCurrency == null)
            {
                preferredCurrency = currencies.FirstOrDefault(c => c.Code == "PLN");
            }

            var productViewModels = products.Select(product => new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Currency = preferredCurrency,
                Price = product.Price,
                ConvertedPrice = Math.Round(product.Price/preferredCurrency.Rate,2)
            }).ToList();

            return View(productViewModels);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
