using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clothing_store.Interfaces;
using clothing_store.Models;


namespace clothing_store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ITaxService _taxService;
        private readonly IBrandService _brandService;
        public ProductController(IProductService productService,ITaxService taxService, IBrandService brandService)
        {
            _productService = productService;
            _taxService = taxService;
            _brandService = brandService;
        }
        // GET: ProductController
        public async Task<ActionResult> Index()
        {
            var Products = await _productService.GetAllProductsAsync();
            return View(Products);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductController/Create
        public async Task<IActionResult> Create()
        {
            var taxes = await _taxService.GetAllTaxesAsync();
            var brands = await _brandService.GetAllBrandsAsync();
            ViewBag.Brands = brands;
            ViewBag.Taxes = taxes;

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var tax = await _taxService.GetTaxByIdAsync(dto.TaxId);
            var brand = await _brandService.GetBrandByIdAsync(dto.BrandId);

            if (brand == null || tax == null)
            {
                ModelState.AddModelError("", "Wybrany Brand lub Vat nie istnieje.");
                return View(dto);
            }

            var product = new Product
            {
                Name = dto.Name,
                BrandId = dto.BrandId,
                Brand = brand,
                Description = dto.Description,
                Views = 0,
                Visible = dto.Visible,
                Price = dto.Price,
                Tax = tax,
                Photos = new List<string>(),
                PinnedFiles = new List<LinkedFile>(),
                New = true,
                TimePosted = DateTime.Now,
                Quantity = dto.Quantity,
                TimesBought = 0,
                InStock = true,
                Opinions = new List<Opinion>()
            };

            await _productService.AddProductAsync(product);

            return RedirectToAction("Index");
        }


        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProductController/Edit/5
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

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProductController/Delete/5
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
