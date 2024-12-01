namespace clothing_store.Models
{
    public class OrderDetailsDto
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public StatusEnum OrderStatus { get; set; }
        public decimal FullPrice { get; set; }
        public string ShippingMethod { get; set; }
        public string PaymentMethod { get; set; }
        public string Address { get; set; }
        public List<OrderProductDto> Products { get; set; }
    }

    public class OrderProductDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
