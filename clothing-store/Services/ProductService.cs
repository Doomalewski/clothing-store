using clothing_store.Interfaces;
using clothing_store.Models;

namespace clothing_store.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICurrencyService _currencyService;
        public ProductService(IProductRepository repository, ICurrencyService currencyService)
        {
            _productRepository = repository;
            _currencyService = currencyService;
        }

        public async Task<Product> GetProductByIdAsync(int productId) => await _productRepository.GetProductByIdAsync(productId);

        public async Task AddProductAsync(Product product) => await _productRepository.AddProductAsync(product);
        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllProductsAsync();
        }
        public async Task UpdateProductAsync(Product product) => await _productRepository.UpdateProductAsync(product);
        public async Task DeleteProductByIdAsync(int productId)
        {
            var productToDelete = await _productRepository.GetProductByIdAsync(productId);

            // Sprawdzenie i ustawienie nowego produktu jako "New" w razie potrzeby
            if (productToDelete.New == true)
            {
                var oldProducts = await _productRepository.GetAllOldProductsAsync();
                // Posortowane od najnowszego do najstarszego
                var productToSetNew = oldProducts.OrderByDescending(x => x.TimePosted).FirstOrDefault();
                if (productToSetNew != null)
                {
                    productToSetNew.New = true;
                    await _productRepository.UpdateProductAsync(productToSetNew);
                }
            }

            // Usunięcie wszystkich zdjęć powiązanych z produktem
            foreach (var photo in productToDelete.Photos)
            {
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", photo);
                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            // Usunięcie produktu z repozytorium
            await _productRepository.DeleteProductAsync(productToDelete);
        }

        public async Task<Product> GetOldestNewProductAsync()
        {
            var newProducts =  await _productRepository.GetAllNewProductsAsync();
            //sorted from old to new
            return newProducts.OrderBy(p => p.TimePosted).FirstOrDefault();
        }
        public async Task<ProductDetailsDto> GetProductDetailsDtoAsync(int id, string preferredCurrencyCode)
        {
            var product = await GetProductByIdAsync(id);
            if (product == null)
            {
                throw new ArgumentException("Product not found.");
            }

            var currencies = await _currencyService.GetAllCurrenciesAsync();
            var preferredCurrency = currencies.FirstOrDefault(c => c.Code == preferredCurrencyCode)
                                    ?? currencies.FirstOrDefault(c => c.Code == "PLN");
            return new ProductDetailsDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Description = product.Description,
                ConvertedPrice = Math.Round(product.Price / preferredCurrency.Rate, 2),
                ConvertedDiscountPrice = product.DiscountPrice.HasValue
                ? Math.Round(product.DiscountPrice.Value / preferredCurrency.Rate, 2)
                : (decimal?)null,
                Currency = preferredCurrency?.Code ?? "PLN",
                InStock = product.InStock,
                Quantity = product.Quantity,
                Photos = product.Photos
            };
        }

    }
}
