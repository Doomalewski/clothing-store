using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace clothing_store.Models
{
    public class BasketProduct
    {
        [Key]
        public int BasketProductId { get; set; } // Klucz główny tabeli pośredniej

        public int BasketId { get; set; } // Klucz obcy do Basket
        [ForeignKey(nameof(BasketId))]
        public virtual Basket Basket { get; set; }

        public int ProductId { get; set; } // Klucz obcy do Product
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity has to be Greater than zero")]
        public int Quantity { get; set; }
    }

}
