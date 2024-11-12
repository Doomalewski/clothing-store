using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clothing_store.Models.Product;

    [NotMapped]
    public class ProductWithQuantity
    {
        [Key]
        [Required]
        public int ProductId { get; set; } // Klucz obcy do encji Product

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Ilość musi być większa niż zero.")]
        public int Quantity { get; set; }
    }

