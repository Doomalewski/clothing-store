using System.ComponentModel.DataAnnotations;

namespace clothing_store.ViewModels
{
    public class PlaceOrderViewModel
    {
        [Required]

        public int AccountId { get; set; }
        [Required]

        public int BasketId { get; set; }

            [Required]

        public int AddressId { get; set; }
        [Required]

        public int ChosenShippingMethodId { get; set; }
        [Required]

        public int ChosenPaymentMethodId { get; set; }
        [Required]

        public bool DifferentAddress { get; set; }

        // Additional fields for a new address
        [Required]

        public string Street { get; set; }
        [Required]

        public string City { get; set; }
        [Required]

        public string State { get; set; }
        [Required]

        public string ZipCode { get; set; }
        [Required]

        public string Country { get; set; }
    }
}
