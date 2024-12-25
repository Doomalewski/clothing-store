using clothing_store.Interfaces;
using clothing_store.Models;
using clothing_store.Services;
using clothing_store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
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
            // Jeœli sortOrder jest null, pobierz je z ciasteczka

                var sortOrder = Request.Cookies["SortOrder"] ?? "default"; // Domyœlnie "default"


            var products = await _productService.GetAllProductsAsync();
            var currencies = await _currencyService.GetAllCurrenciesAsync();

            var preferredCurrencyCode = Request.Cookies["PreferredCurrency"] ?? "PLN";
            var preferredCurrency = currencies.FirstOrDefault(c => c.Code == preferredCurrencyCode) ?? currencies.FirstOrDefault(c => c.Code == "PLN");

            products = await GetSortedProductsAsync(sortOrder);
            TempData["SortOrder"] = sortOrder;
            var productViewModels = products.Select(product => new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Currency = preferredCurrency,
                Price = product.Price,
                ConvertedPrice = Math.Round(product.Price / preferredCurrency.Rate, 2),
                New = product.New,
                Quantity = product.Quantity
            }).ToList();

            return View(productViewModels);
        }
        private async Task<List<Product>> GetSortedProductsAsync(string sortOrder)
        {
            var products= await _productService.GetAllProductsAsync();
            return sortOrder switch
            {
                "priceAsc" => products.OrderBy(p => p.Price).ToList(),
                "priceDesc" => products.OrderByDescending(p => p.Price).ToList(),
                "nameAsc" => products.OrderBy(p => p.Name).ToList(),
                "nameDesc" => products.OrderByDescending(p => p.Name).ToList(),
                "timesBoughtDesc" => products.OrderByDescending(p => p.TimesBought).ToList(),
                _ => products.ToList(), // Domyœlne sortowanie
            };
        }

        public async Task<IActionResult> Products()
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
            var sortOrder = Request.Cookies["SortOrder"] ?? "default"; // Domyœlnie "default"

            TempData["SortOrder"] = sortOrder;
            products = await GetSortedProductsAsync(sortOrder);

            var productViewModels = products.Select(product => new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Currency = preferredCurrency,
                Price = product.Price,
                ConvertedPrice = Math.Round(product.Price / preferredCurrency.Rate, 2),
                New = product.New,
                Quantity = product.Quantity
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
