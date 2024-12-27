using clothing_store.Interfaces;
using clothing_store.Models;

namespace clothing_store.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
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
    }
}
