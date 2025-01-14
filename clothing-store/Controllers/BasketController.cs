using clothing_store.Interfaces;
using clothing_store.Interfaces.clothing_store.Interfaces;
using clothing_store.Models;
using clothing_store.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace clothing_store.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IAccountService _accountService;
        private readonly IProductService _productService;
        private readonly ICurrencyService _currencyService;
        public BasketController(IBasketService basketService,IAccountService accountService,IProductService productService, ICurrencyService currencyService)
        {
            _basketService = basketService;
            _accountService = accountService;
            _productService = productService;
            _currencyService = currencyService;
        }
        
        public IActionResult Checkout()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult ProcessCheckout(string fullName, string email, string address, string city, string postalCode, string phoneNumber, string paymentMethod)
        {
            if (ModelState.IsValid)
            {
                // Process the order (e.g., save to database, send email, etc.)

                // Redirect to the main page after successfully placing the order
                return RedirectToAction("Index", "Home");
            }

            // If the form submission is invalid, return the same checkout view
            TempData["Error"] = "There was an issue with your order. Please check your input and try again.";
            return View("Checkout");
        }
        public async Task<IActionResult> Index()
        {
            var accountIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (accountIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int accountId = int.Parse(accountIdClaim.Value);
            var currencies = await _currencyService.GetAllCurrenciesAsync();

            var preferredCurrencyCode = Request.Cookies["PreferredCurrency"] ?? "PLN";
            var preferredCurrency = currencies.FirstOrDefault(c => c.Code == preferredCurrencyCode) ?? currencies.FirstOrDefault(c => c.Code == "PLN");
            var items = await _basketService.GetBasketByAccountIdAsync(accountId);
            var BasketDto = new BasketIndexDto
            {
                basket = items,
                currency = preferredCurrency
            };
            return View(BasketDto);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCart(int productId, string action)
        {
            var account = await _accountService.GetAccountFromHttpAsync();
            if (account == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var basketProduct = await _basketService.GetBasketProductByIdAsync(account.AccountId, productId);
            if(action == "increment")
            {
                var productToUpdate = await _productService.GetProductByIdAsync(productId);
                if (basketProduct != null && basketProduct.Quantity >= productToUpdate.Quantity)
                {
                    TempData["StockAlert"] = $"You cannot add more than {productToUpdate.Quantity} units of this product.";
                    return RedirectToAction("Index", "Basket");
                }
            }
            if (basketProduct.Quantity <= 0)
            {
                TempData["StockAlert"] = "Product is sold out";
                return RedirectToAction("Details", "Product", new { id = basketProduct.ProductId }); // Przekierowanie na szczegóły produktu
            }
            if (basketProduct != null)
            {
                switch (action)
                {
                    case "increment":
                        await _basketService.UpdateProductQuantityAsync(account.AccountId, productId, basketProduct.Quantity + 1); // Inkrementacja ilości
                        break;
                    case "decrement":
                        if (basketProduct.Quantity > 1) // Sprawdź, czy ilość nie spadnie poniżej 1
                        {
                            await _basketService.UpdateProductQuantityAsync(account.AccountId, productId, basketProduct.Quantity - 1); // Dekrementacja ilości
                        }
                        break;
                }
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var accountIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (accountIdClaim == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int accountId = int.Parse(accountIdClaim.Value);

            // Usuń produkt z koszyka
            await _basketService.RemoveProductFromBasketAsync(accountId, productId);

            // Przekierowanie z powrotem do koszyka
            return RedirectToAction("Index");
        }
    }


}
