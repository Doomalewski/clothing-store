namespace clothing_store.Models
{
    public class ProductDetailsDto
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal ConvertedPrice { get; set; }
        public decimal? ConvertedDiscountPrice { get; set; }
        public string Currency { get; set; } = "PLN";
        public bool InStock { get; set; }
        public int Quantity { get; set; }
        public List<string> Photos { get; set; } = new List<string>();
        public bool IsDiscounted => ConvertedDiscountPrice.HasValue && ConvertedDiscountPrice > 0;
    }

}
