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
