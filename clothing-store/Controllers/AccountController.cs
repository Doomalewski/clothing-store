using clothing_store.Interfaces;
using clothing_store.Interfaces.clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clothing_store.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICurrencyService _currencyService;
        private readonly IBasketService _basketService;
        public AccountController(IAccountService accountService,ICurrencyService currencyService,IBasketService basketService)
        {
            _accountService = accountService;
            _currencyService = currencyService;
            _basketService = basketService;
        }
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
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
            var account = new Account
            {
                Username = model.Username,
                Email = model.Email,
                Password = _accountService.SaltAndHashPassword(model.Password),
                Orders = new List<Order>(),
                Basket = new Basket(),
                Name = string.Empty,
                Surname = string.Empty,
                Address = null,
                Discounts = new List<SpecialDiscount>(),
                CorporateClient = false,
                Newsletter = false
            };

            var basket = new Basket
            {
                Account = account,
                BasketProducts = new List<BasketProduct>()  // Inicjalizacja pustej listy produktów w koszyku
            };

            // Po zapisaniu koszyka, przypisujemy BasketId do konta
            account.BasketId = basket.BasketId;

            await _accountService.AddAccountAsync(account);

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
            var basketToDelete = await _basketService.GetBasketByIdAsync(account.BasketId);

            await _basketService.DeleteBasketAsync(basketToDelete);
            await _accountService.DeleteAccountAsync(account);

            // Redirect to a confirmation page or home page after deletion
            return RedirectToAction("Index", "Home");
        }

    }
}
