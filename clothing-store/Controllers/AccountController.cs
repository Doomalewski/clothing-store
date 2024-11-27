 using clothing_store.Interfaces;
using clothing_store.Interfaces.clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace clothing_store.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICurrencyService _currencyService;
        private readonly IBasketService _basketService;
        private readonly IProductService _productService;
        private readonly IEmailService _emailService;
        public AccountController(IAccountService accountService,ICurrencyService currencyService,IBasketService basketService,IProductService productService, IEmailService emailService)
        {
            _accountService = accountService;
            _currencyService = currencyService;
            _basketService = basketService;
            _productService = productService;
            _emailService = emailService;
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
                Address = null,
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

    }
}
