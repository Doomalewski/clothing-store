using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface IProductService
    {
        public Task<Product> GetProductByIdAsync(int productId);
        public Task AddProductAsync(Product product);
        public Task<List<Product>> GetAllProductsAsync();
        public Task UpdateProductAsync(Product product);
        public Task DeleteProductByIdAsync(int productId);
    }
}
