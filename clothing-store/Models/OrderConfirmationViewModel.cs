namespace clothing_store.ViewModels
{
    public class OrderConfirmationViewModel
    {
        public int OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public decimal FullPrice { get; set; }
        public string ShippingMethodName { get; set; }
        public string PaymentMethodName { get; set; }
        public string OrderStatus { get; set; }
    }
}