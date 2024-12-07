﻿using clothing_store.Interfaces;
using clothing_store.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace clothing_store.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ICurrencyService _currencyService; // Dodajemy ICurrencyService
        private readonly IProductService _productService;
        private readonly IPDFService _pdfService;
        public AdminController(IAccountService accountService, ICurrencyService currencyService, IProductService productService, IPDFService pdfService)
        {
            _accountService = accountService;
            _currencyService = currencyService;
            _productService = productService;
            _pdfService = pdfService;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await _accountService.GetAllAccountsAsync();
            return View(accounts);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Account account)
        {
            if (id != account.AccountId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                //await _accountService.UpdateAccountAsync(account);
                return RedirectToAction(nameof(Index));
            }

            return View(account);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // await _accountService.DeleteAccountAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // Akcja do ręcznego aktualizowania kursów walut
        public async Task<IActionResult> UpdateCurrencyRates()
        {
            await _currencyService.UpdateCurrencyRatesAsync();
            TempData["Message"] = "Kursy walut zostały zaktualizowane."; // Komunikat o sukcesie
            return RedirectToAction(nameof(Dashboard)); // Powrót na dashboard
        }
        [HttpGet]
        public async Task<IActionResult> DownloadPriceList()
        {
            var products = await  _productService.GetAllProductsAsync();

            var pdf = _pdfService.GenerateProductPriceListPdf(products);

            return File(pdf, "application/pdf", "ProductPriceList.pdf");
        }
    }
}
