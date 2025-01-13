namespace clothing_store.Models
{
    public class OrderListDto
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public StatusEnum OrderStatus { get; set; }
        public decimal FullPrice { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethod { get; set; }
    }

}
