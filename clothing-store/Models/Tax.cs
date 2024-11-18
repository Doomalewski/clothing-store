using System.ComponentModel.DataAnnotations;

namespace clothing_store.Models
{
    public class Tax
    {
        [Key]
        public int TaxId { get; set; }

        [Required(ErrorMessage = "Tax name is required.")]
        [StringLength(100, ErrorMessage = "Tax name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        [Range(0, 100, ErrorMessage = "Tax value must be between 0 and 100.")]
        public int Value { get; set; }

    }
}
