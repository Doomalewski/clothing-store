namespace clothing_store.Models
{
    public class OrderSummaryDto
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public StatusEnum OrderStatus { get; set; }
        public decimal FullPrice { get; set; }
    }

}
