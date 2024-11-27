 using clothing_store.Interfaces;
using clothing_store.Interfaces.clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using clothing_store.ViewModels;
using clothing_store.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace clothing_store.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICurrencyService _currencyService;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IEmailService _emailService;
        private readonly IOrderService _orderService;
        public AccountController(IAccountService accountService,ICurrencyService currencyService,IBasketService basketService,IProductService productService, IEmailService emailService,IOrderService orderService)
        {
            _accountService = accountService;
            _currencyService = currencyService;
            _basketService = basketService;
            _productService = productService;
            _emailService = emailService;
            _orderService = orderService;
        }
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }


        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(AccountDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var isEmailTaken = await _accountService.IsEmailInUse(model.Email);
            if (isEmailTaken)
            {
                ModelState.AddModelError("Email", "Email is already in use.");
                return View(model);
            }
            var basket = new Basket();

            var account = new Account
            {
                Username = model.Username,
                Email = model.Email,
                Password = _accountService.SaltAndHashPassword(model.Password),
                Orders = new List<Order>(),
                Basket = basket,
                Name = string.Empty,
                Surname = string.Empty,
                Address = new Address() // Pusty adres przypisany do konta
                {
                    Street = string.Empty,
                    City = string.Empty,
                    State = string.Empty,
                    ZipCode = string.Empty,
                    Country = string.Empty
                },
                Discounts = new List<SpecialDiscount>(),
                CorporateClient = false,
                Newsletter = false,
                Role = "User"
            };
            await _accountService.AddAccountAsync(account);
            await _emailService.SendConfirmationEmail(model.Username);

            return RedirectToAction("Login", "Account");
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, string confirmPhrase)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            if (confirmPhrase != "deletenow")
            {
                ModelState.AddModelError("confirmPhrase", "The confirmation phrase is incorrect.");
                return View(account);
            }

            await _accountService.DeleteAccountAsync(account);

            // Redirect to a confirmation page or home page after deletion
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            // Sign out the user from the authentication system
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to the home page or login page
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var account = await _accountService.GetAccountByEmailAsync(model.Email);
            if (account == null)
            {
                TempData["LoginError"] = "Invalid email or password.";
                return View(model);
            }

            bool valid = _accountService.VerifyPassword(account.Password, model.Password);
            if (!valid)
            {
                TempData["LoginError"] = "Invalid email or password.";
                return View(model);
            }

            // Set up claims based on the account details
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new Claim(ClaimTypes.Role, account.Role)  // Add role claim
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Sign the user in by setting the authentication cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Redirect based on the role
            if (account.Role == "Admin")
            {
                return RedirectToAction("Dashboard", "Admin");
            }
            else if (account.Role == "User")
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Details()
        {
            // Pobierz AccountId z ról lub z claims, jeśli użytkownik jest zalogowany
            var accountIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            // Sprawdź, czy AccountId jest dostępne w claims
            if (accountIdClaim == null)
            {
                return RedirectToAction("Login", "Account"); // Przekierowanie do logowania, jeśli brak ID
            }

            // Zamień AccountId na int
            int accountId = int.Parse(accountIdClaim.Value);

            // Pobierz dane konta na podstawie AccountId
            var account = await _accountService.GetAccountByIdAsync(accountId);

            // Jeśli konto nie istnieje, zwróć 404
            if (account == null)
            {
                return NotFound();
            }

            // Przekaż dane do widoku
            return View(account);
        }
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(int id)
        {
            // Pobierz produkt po ID
            var productToAdd = await _productService.GetProductByIdAsync(id);
            if (productToAdd == null)
            {
                return NotFound(); // Produkt nie istnieje
            }

            // Pobierz ID zalogowanego użytkownika
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized(); // Brak zalogowanego użytkownika
            }

            int userId = int.Parse(userIdClaim.Value);

            // Dodaj produkt do koszyka użytkownika
            await _basketService.AddProductToCartAsync(userId, productToAdd);

            return RedirectToAction("Index","Home"); // Możesz zwrócić inną odpowiedź, np. Redirect
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> PlaceOrder()
        {
            // Pobranie danych konta
            var account = await _accountService.GetAccountFromHttpAsync();
            if (account == null)
            {
                return RedirectToAction("Login", "Account"); // Przekierowanie, jeśli użytkownik nie jest zalogowany
            }

            // Pobranie koszyka
            var basket = await _basketService.GetBasketByAccountIdAsync(account.AccountId);
            if (basket == null || !basket.BasketProducts.Any())
            {
                return RedirectToAction("Index", "Basket"); // Przekierowanie, jeśli koszyk jest pusty
            }
            var address = await _accountService.GetAddressByIdAsync(account.AddressId);
            ViewBag.shippingMethods = await _orderService.GetAllShippingMethodsAsync();
            ViewBag.paymentMethods = await _orderService.GetAllPaymentMethodsAsync();

            // Przygotowanie modelu widoku
            var model = new PlaceOrderViewModel
            {
                AccountId = account.AccountId,
                BasketId = basket.BasketId,
                AddressId = account.AddressId,
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(PlaceOrderViewModel model)
        {
            // 1. Jeśli użytkownik nie wybrał innego adresu, przypisz dane adresowe z konta
            if (!model.DifferentAddress)
            {
                var address = await _accountService.GetAddressByIdAsync(model.AddressId);
                model.City = address.City;
                model.Country = address.Country;
                model.State = address.State;
                model.ZipCode = address.ZipCode;
                model.Street = address.Street;
            }

            // 2. Pobranie konta na podstawie AccountId
            var account = await _accountService.GetAccountByIdAsync(model.AccountId);
            if (account == null)
            {
                ModelState.AddModelError("", "Account not found.");
                return View(model);
            }

            // 3. Pobranie koszyka na podstawie BasketId
            var basket = await _basketService.GetBasketByIdAsync(model.BasketId);
            if (basket == null || !basket.BasketProducts.Any())
            {
                ModelState.AddModelError("", "Basket is empty.");
                return View(model);
            }

            var shippingMethod = await _orderService.GetShippingMethodByIdAsync(model.ChosenShippingMethodId);
            var paymentMethod = await _orderService.GetPaymentMethodByIdAsync(model.ChosenPaymentMethodId);
            // 4. Sprawdzanie, czy wszystkie pola adresowe są wypełnione, jeśli wybrano nowy adres
            if (model.DifferentAddress && (string.IsNullOrEmpty(model.Street) || string.IsNullOrEmpty(model.City) ||
                                            string.IsNullOrEmpty(model.State) || string.IsNullOrEmpty(model.ZipCode) ||
                                            string.IsNullOrEmpty(model.Country)))
            {
                ModelState.AddModelError("", "All address fields are required when choosing a new address.");
                return View(model);
            }

            // 5. Utworzenie zamówienia
            var order = new Order
            {
                AccountId = model.AccountId,
                Street = model.Street,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                Country = model.Country,
                FullPrice = basket.BasketProducts.Sum(bp => bp.Quantity * bp.Product.Price) + (decimal)shippingMethod.Price, // Dodaj koszty wysyłki (np. z modelu ShippingMethod)
                OrderStatus = StatusEnum.New, // Ustaw status zamówienia na 'Pending'
                Shipping = shippingMethod, // Zakładając, że ShippingMethod to pole w modelu PlaceOrderViewModel
                Payment = paymentMethod, // Zakładając, że PaymentMethod to pole w modelu PlaceOrderViewModel
                Date = DateTime.UtcNow // Ustawienie daty zamówienia
            };

            // 6. Zapisanie zamówienia w bazie danych
            await _orderService.AddOrderAsync(order);

            // 7. Dodanie produktów do zamówienia
            foreach (var item in basket.BasketProducts)
            {
                var orderProduct = new OrderProduct
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };

                //_orderService.AddOrderProductAsync(orderProduct);
            }
            //await _context.SaveChangesAsync();

            // 8. Po zapisaniu zamówienia, opróżnianie koszyka
            await _accountService.ClearBasketAsync(model.AccountId);

            // 9. Przekierowanie do strony potwierdzenia zamówienia
            return RedirectToAction("OrderConfirmation", new { orderId = order.OrderId });
        }



    }
}
