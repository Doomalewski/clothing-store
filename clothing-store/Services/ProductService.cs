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
            string productPhotoName = productToDelete.Name + "1.jpg";
            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", $"{productPhotoName}");

            // Sprawdzenie, czy plik istnieje, i jego usunięcie
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }
            await _productRepository.DeleteProductAsync(productToDelete);
        }
    }
}
