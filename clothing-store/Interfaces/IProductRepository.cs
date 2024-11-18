using clothing_store.Models;

namespace clothing_store.Interfaces
{
    public interface IProductRepository
    {
        public Task AddProductAsync(Product product);
        public Task<Product> GetProductByIdAsync(int Id);
        public Task<List<Product>> GetAllProductsAsync();

    }
}
