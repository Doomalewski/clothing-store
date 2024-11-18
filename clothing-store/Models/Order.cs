using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        public int AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public Account ClientDetails { get; set; }

        [Required]
        public Address Address { get; set; }

        public List<ProductWithQuantity> Products { get; set; } = new();

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Cena produktów nie może być ujemna.")]
        public int ProductsPrice { get; set; }

        [Required]
        public StatusEnum OrderStatus { get; set; }

        [Required]
        public ShippingMethod Shipping { get; set; }

        [Required]
        public PaymentMethod Payment { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Pełna cena nie może być ujemna.")]
        public int FullPrice { get; set; }
    }

