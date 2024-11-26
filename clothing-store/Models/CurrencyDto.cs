namespace clothing_store.Models
{
    public class CurrencyDto
    {
        public string Name { get; set; }  // Nazwa waluty
        public string Code { get; set; }  // Kod waluty (np. USD, EUR)
        public decimal Rate { get; set; } // Kurs waluty
    }

}
