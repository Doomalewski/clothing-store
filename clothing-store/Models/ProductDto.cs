namespace clothing_store.Models
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int TaxId { get; set; }
        public bool Visible { get; set; }
        public int Quantity { get; set; }

        public List<IFormFile> Photos { get; set; } = new List<IFormFile>();

    }

}
