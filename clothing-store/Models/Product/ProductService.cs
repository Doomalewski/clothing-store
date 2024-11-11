namespace clothing_store.Models.Product
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Product? GetProductById(int productId) => _repository.GetById(productId);

        public IEnumerable<Product> GetAllProducts() => _repository.GetAll();

        public void CreateProduct(Product product)
        {
            _repository.Add(product);
        }

        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
        }

        public void DeleteProduct(int productId)
        {
            _repository.Delete(productId);
        }
    }
}
