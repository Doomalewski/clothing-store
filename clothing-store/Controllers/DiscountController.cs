using Microsoft.AspNetCore.Mvc;

namespace clothing_store.Controllers
{
    using clothing_store.Interfaces;
    using clothing_store.Services;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class SpecialDiscountController : Controller
    {
        private readonly IDiscountService _discountService;
        private readonly IAccountService _accountService;

        public SpecialDiscountController(IDiscountService discountService, IAccountService accountService)
        {
            _discountService = discountService;
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult CreateSpecialDiscount(int accountId)

        {
            ViewBag.AccountId = accountId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpecialDiscountPost(SpecialDiscount specialDiscount)
        {
            if (!ModelState.IsValid)
            {
                return View(specialDiscount);
            }
            if (specialDiscount.EndTime.Kind == DateTimeKind.Unspecified)
            {
                specialDiscount.EndTime = DateTime.SpecifyKind(specialDiscount.EndTime, DateTimeKind.Utc);
            }

            await _discountService.CreateSpecialDiscountAsync(specialDiscount);
                return RedirectToAction("Index", "SpecialDiscount");
        }
    }

}
