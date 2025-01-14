using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clothing_store.Interfaces;
using clothing_store.Models;
using clothing_store.ViewModels;
using NuGet.Versioning;


namespace clothing_store.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ITaxService _taxService;
        private readonly IBrandService _brandService;
        private readonly ICurrencyService _currencyService;
        public ProductController(IProductService productService,ITaxService taxService, IBrandService brandService, ICurrencyService currencyService )
        {
            _productService = productService;
            _taxService = taxService;
            _brandService = brandService;
            _currencyService = currencyService;
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
                // Obsługa do maksymalnie 5 zdjęć
                int maxPhotos = Math.Min(dto.Photos.Count, 5);
                for (int i = 0; i < maxPhotos; i++)
                {
                    var photo = dto.Photos[i];
                    if (photo.Length > 0)
                    {
                        // Generowanie nowej nazwy pliku na podstawie nazwy produktu
                        var newFileName = $"{dto.Name}{i + 1}.jpg";

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
                ModelState.AddModelError("", "Wybrany Brand lub VAT nie istnieje.");
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



        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var preferredCurrencyCode = Request.Cookies["PreferredCurrency"] ?? "PLN";
                var productDetails = await _productService.GetProductDetailsDtoAsync(id, preferredCurrencyCode);
                return View(productDetails);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Repopulate dropdown data if validation fails
                model.Brands = await _brandService.GetAllBrandsAsync();
                model.Taxes = await _taxService.GetAllTaxesAsync();
                return View(model);
            }

            var product = await _productService.GetProductByIdAsync(model.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            // Updating product properties
            product.Name = model.Name;
            product.BrandId = model.BrandId;
            product.Description = model.Description;
            product.Price = model.Price;
            product.DiscountPrice = model.DiscountPrice;
            product.TaxId = model.TaxId;
            product.Quantity = model.Quantity;
            product.Visible = model.Visible;
            product.New = model.New;
            product.InStock = model.InStock;

            await _productService.UpdateProductAsync(product);

            TempData["Message"] = "Product updated successfully!";
            return RedirectToAction(nameof(Index));
        }

        // GET: ProductController/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var productToDelete = await _productService.GetProductByIdAsync(id);
            return View(productToDelete);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var taxes = await _taxService.GetAllTaxesAsync();

            // Append % to the Value property
            var taxOptions = taxes.Select(t => new
            {
                t.TaxId,
                Value = $"{t.Value}%"
            }).ToList();

            var viewModel = new ProductEditViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                BrandId = product.BrandId,
                Description = product.Description,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                TaxId = product.TaxId,
                Quantity = product.Quantity,
                Visible = product.Visible,
                New = product.New,
                InStock = product.InStock,
                Brands = await _brandService.GetAllBrandsAsync(),
                Taxes = taxOptions
            };

            return View(viewModel);
        }


        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProductByIdAsync(id);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> EditDiscount(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var viewModel = new EditDiscountViewModel
            {
                ProductId = product.ProductId,
                Name = product.Name,
                CurrentPrice = product.Price,
                DiscountPrice = product.DiscountPrice
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDiscount(EditDiscountViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var product = await _productService.GetProductByIdAsync(model.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            product.DiscountPrice = model.DiscountPrice;

            await _productService.UpdateProductAsync(product);

            TempData["Message"] = "Discount updated successfully!";
            return RedirectToAction("Index"); // Adjust the redirect as needed
        }



    }
}
