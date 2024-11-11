namespace clothing_store.Models.Product
{
    public interface IProductRepository
    {
        Product? GetById(int productId);
        IEnumerable<Product> GetAll();
        void Add(Product product);
        void Update(Product product);
        void Delete(int productId);
    }
}
