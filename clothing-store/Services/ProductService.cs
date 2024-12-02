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

    }
}
