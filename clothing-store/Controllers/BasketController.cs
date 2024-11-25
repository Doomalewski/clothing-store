using clothing_store.Interfaces.clothing_store.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace clothing_store.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
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

            var items = await _basketService.GetBasketByAccountIdAsync(accountId);
            return View(items);
        }
    }
    
    
}
