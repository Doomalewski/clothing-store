namespace clothing_store.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; } // Currency for price
        public decimal ConvertedPrice { get; set; } // Converted price
        public decimal? ConvertedDiscountPrice { get; set; } // Nullable discount price
        public bool New { get; set; }
        public int Quantity { get; set; }
        public bool IsDiscounted => ConvertedDiscountPrice.HasValue && ConvertedDiscountPrice > 0; // Check if product has a discount
    }
}
