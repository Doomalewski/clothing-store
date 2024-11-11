namespace clothing_store.Models.Product
{
    public interface IProductService
    {
        Product? GetProductById(int productId);
        IEnumerable<Product> GetAllProducts();
        void CreateProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int productId);
    }
}
