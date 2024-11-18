namespace clothing_store.Models
{
    public class ProductCreateViewModel
    {
        public Product Product { get; set; }
        public List<Tax> Taxes { get; set; }
        public List<Brand> Brands { get; set; }
    }
}
