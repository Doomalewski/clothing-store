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

        public async Task<IActionResult> Index(string? searchQuery)
        {
            // Jeœli sortOrder jest null, pobierz je z ciasteczka
            var sortOrder = Request.Cookies["SortOrder"] ?? "default"; // Domyœlnie "default"

            var currencies = await _currencyService.GetAllCurrenciesAsync();
            var preferredCurrencyCode = Request.Cookies["PreferredCurrency"] ?? "PLN";
            var preferredCurrency = currencies.FirstOrDefault(c => c.Code == preferredCurrencyCode)
                                     ?? currencies.FirstOrDefault(c => c.Code == "PLN");
            var productstoSort = await _productService.GetAllProductsAsync();
            var products = GetSortedProductsAsync(sortOrder, productstoSort);

            // Filtracja na podstawie wyszukiwanego zapytania
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                                             || (p.Description?.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ?? false))
                                   .ToList();
            }

            var topProducts = products
                .OrderByDescending(p => p.TimesBought)
                .Take(10)
                .ToList();

            TempData["SortOrder"] = sortOrder;
            ViewData["SearchQuery"] = searchQuery;

            var productViewModels = topProducts.Select(product => new ProductViewModel
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

        public async Task<IActionResult> Products(int pageNumber = 1, int pageSize = 5)
        {
            // Pobierz dostêpne waluty
            var currencies = await _currencyService.GetAllCurrenciesAsync();
            var preferredCurrencyCode = Request.Cookies["PreferredCurrency"] ?? "PLN";
            var preferredCurrency = currencies.FirstOrDefault(c => c.Code == preferredCurrencyCode);
            if (preferredCurrency == null)
            {
                preferredCurrency = currencies.FirstOrDefault(c => c.Code == "PLN");
            }

            // Pobierz listê produktów z paginacj¹
            var products = await _productService.GetPaginatedProductsAsync(pageNumber, pageSize);

            // Sortowanie (opcjonalne, jeœli jest potrzebne)
            var sortOrder = Request.Cookies["SortOrder"] ?? "default"; // Domyœlnie "default"
            TempData["SortOrder"] = sortOrder;

            // Zastosuj sortowanie po paginacji
            products = GetSortedProductsAsync(sortOrder, products).ToList();

            // Zliczanie ³¹cznej liczby produktów, aby obliczyæ liczbê stron
            var totalProducts = await _productService.GetTotalProductCountAsync();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);  // Obliczanie liczby stron

            // Tworzenie ViewModelu
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

            // Przekazanie danych do widoku
            var viewModel = new ProductListViewModel
            {
                Products = productViewModels,
                CurrentPage = pageNumber,
                TotalPages = totalPages,
                PageSize = pageSize
            };

            return View(viewModel);
        }

        // Update GetSortedProductsAsync to accept already paginated products
        private IEnumerable<Product> GetSortedProductsAsync(string sortOrder, List<Product> products)
        {
            return sortOrder switch
            {
                "priceAsc" => products.OrderBy(p => p.Price),
                "priceDesc" => products.OrderByDescending(p => p.Price),
                "nameAsc" => products.OrderBy(p => p.Name),
                "nameDesc" => products.OrderByDescending(p => p.Name),
                "timesBoughtDesc" => products.OrderByDescending(p => p.TimesBought),
                _ => products, // Domyœlne sortowanie
            };
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
