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
            // Je�li sortOrder jest null, pobierz je z ciasteczka
            var sortOrder = Request.Cookies["SortOrder"] ?? "default"; // Domy�lnie "default"

            var currencies = await _currencyService.GetAllCurrenciesAsync();
            var preferredCurrencyCode = Request.Cookies["PreferredCurrency"] ?? "PLN";
            var preferredCurrency = currencies.FirstOrDefault(c => c.Code == preferredCurrencyCode)
                                             ?? currencies.FirstOrDefault(c => c.Code == "PLN");

            // Pobierz wszystkie produkty
            var products = await _productService.GetAllProductsAsync();

            // Filtracja na podstawie wyszukiwanego zapytania
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                                             || (p.Description?.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ?? false))
                                   .ToList();
            }

            // Sortowanie po filtracji
            products = GetSortedProductsAsync(sortOrder, products).Where(p => p.Visible == true).ToList();

            // Pobranie top 10 produkt�w (najcz�ciej kupowane)
            var topProducts = products
                .OrderByDescending(p => p.TimesBought)
                .Take(10)
                .ToList();

            TempData["SortOrder"] = sortOrder;
            ViewData["SearchQuery"] = searchQuery;

            // Przekszta�cenie produkt�w na widokowy model
            // Przekszta�cenie produkt�w na widokowy model
            var productViewModels = topProducts.Select(product => new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Currency = preferredCurrency,
                Price = product.Price,
                ConvertedPrice = Math.Round(product.Price / preferredCurrency.Rate, 2),
                ConvertedDiscountPrice = product.DiscountPrice.HasValue
                    ? Math.Round(product.DiscountPrice.Value / preferredCurrency.Rate, 2)
                    : (decimal?)null,
                New = product.New,
                Quantity = product.Quantity
            }).ToList();


            return View(productViewModels);
        }


        public async Task<IActionResult> Products(string? searchQuery, int pageNumber = 1, int pageSize = 5)
        {
            // Pobierz dost�pne waluty
            var currencies = await _currencyService.GetAllCurrenciesAsync();
            var preferredCurrencyCode = Request.Cookies["PreferredCurrency"] ?? "PLN";
            var preferredCurrency = currencies.FirstOrDefault(c => c.Code == preferredCurrencyCode);
            if (preferredCurrency == null)
            {
                preferredCurrency = currencies.FirstOrDefault(c => c.Code == "PLN");
            }

            // Pobierz ca�� list� produkt�w
            var products = await _productService.GetAllProductsAsync();

            // Filtracja na podstawie wyszukiwanego zapytania
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                products = products.Where(p => p.Name.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)
                                             || (p.Description?.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ?? false))
                                   .ToList();
            }

            // Sortowanie (opcjonalne, je�li jest potrzebne)
            var sortOrder = Request.Cookies["SortOrder"] ?? "default"; // Domy�lnie "default"
            TempData["SortOrder"] = sortOrder;

            // Zastosuj sortowanie po filtracji
            products = GetSortedProductsAsync(sortOrder, products).Where(p=>p.Visible==true).ToList();

            // Zliczanie ��cznej liczby produkt�w, aby obliczy� liczb� stron
            var totalProducts = products.Count();
            var totalPages = (int)Math.Ceiling((double)totalProducts / pageSize);  // Obliczanie liczby stron

            // Zastosuj paginacj�
            products = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            // Tworzenie ViewModelu
            // Przekszta�cenie produkt�w na widokowy model
            var productViewModels = products.Select(product => new ProductViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Currency = preferredCurrency,
                Price = product.Price,
                ConvertedPrice = Math.Round(product.Price / preferredCurrency.Rate, 2),
                ConvertedDiscountPrice = product.DiscountPrice.HasValue
                    ? Math.Round(product.DiscountPrice.Value / preferredCurrency.Rate, 2)
                    : (decimal?)null, // Handle nullable discount price
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
                _ => products, // Domy�lne sortowanie
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
