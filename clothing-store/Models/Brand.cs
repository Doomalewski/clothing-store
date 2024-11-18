using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace clothing_store.Models
{
    public class Brand
    {
        [Key]
        [Required(ErrorMessage = "BrandId name is required.")]

        public int BrandId { get; set; }

        [Required(ErrorMessage = "Brand name is required.")]
        [StringLength(100, ErrorMessage = "Brand name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;

        [Range(1000, int.MaxValue, ErrorMessage = "Year of foundation must be after 1000.")]
        public int YearOfFoundation { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; } = string.Empty;
    }
}

