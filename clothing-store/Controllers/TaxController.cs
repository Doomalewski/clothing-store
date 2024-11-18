using clothing_store.Interfaces;
using clothing_store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace clothing_store.Controllers
{
    public class TaxController : Controller
    {
        private readonly ITaxService _taxService;
        public TaxController(ITaxService taxService)
        {
            _taxService = taxService;
        }
        // GET: TaxController
        public async Task<ActionResult> Index()
        {
            var Taxes = await _taxService.GetAllTaxesAsync();
            return View(Taxes);
        }

        // GET: TaxController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TaxController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TaxController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Tax tax)
        {
            if (ModelState.IsValid)
            {
                await _taxService.AddTaxAsync(tax);
                return RedirectToAction(nameof(Index));
            }
            return View(tax);
        }

        // GET: TaxController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaxController/Edit/5
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

        // GET: TaxController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaxController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
