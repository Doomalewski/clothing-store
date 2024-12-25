namespace clothing_store.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public Currency Currency { get; set; } // Waluta dla ceny
        public decimal ConvertedPrice { get; set; } // Przeliczona cena
        public bool New { get; set; }
        public int Quantity { get;set; }
    }
}
