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
            var uploadedFileNames = new List<string>();

            // Sprawdzanie czy są załączone zdjęcia
            if (dto.Photos != null && dto.Photos.Count > 0)
            {
                foreach (var photo in dto.Photos)
                {
                    if (photo.Length > 0)
                    {
                        // Generowanie nowej nazwy pliku na podstawie nazwy produktu
                        var newFileName = $"{dto.Name}{uploadedFileNames.Count + 1}{".jpg"}";

                        // Ścieżka do zapisu pliku
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", newFileName);

                        // Zapisanie pliku na dysku
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await photo.CopyToAsync(stream);
                        }

                        // Dodanie nazwy pliku do listy
                        uploadedFileNames.Add(newFileName);
                    }
                }
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
                Photos = uploadedFileNames,
                PinnedFiles = new List<LinkedFile>(),
                New = true,
                TimePosted = DateTime.UtcNow,
                Quantity = dto.Quantity,
                TimesBought = 0,
                InStock = true,
                Opinions = new List<Opinion>()
            };
            var productToRemoveFromNew = await _productService.GetOldestNewProductAsync();
            productToRemoveFromNew.New = false;
            await _productService.UpdateProductAsync(productToRemoveFromNew);
            await _productService.AddProductAsync(product);

            return RedirectToAction("Index");
        }


        // GET: ProductController/Edit/5
        public async Task<ActionResult> Details(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return View(product);
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
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var productToDelete = await _productService.GetProductByIdAsync(id);
            return View(productToDelete);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
