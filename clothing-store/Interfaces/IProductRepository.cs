using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface IProductRepository
    {
        public Task AddProductAsync(Product product);
        public Task<Product> GetProductByIdAsync(int Id);
        public Task<List<Product>> GetAllProductsAsync();
        public Task UpdateProductAsync(Product product);
        public Task DeleteProductAsync(Product product);
        public Task<List<Product>> GetAllNewProductsAsync();
        public Task<List<Product>> GetAllOldProductsAsync();
    }
}
