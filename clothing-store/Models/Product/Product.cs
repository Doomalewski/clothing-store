using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace clothing_store.Models.Product
{
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int BrandId;
        [ForeignKey(nameof(BrandId))]
        public Brand? Brand { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string? Description { get; set; }

        [Required]
        public Price Price { get; set; }

        public int Views { get; set; }

        [Required]
        public bool Visible { get; set; }

        [MinLength(1, ErrorMessage = "At least one photo is required.")]
        public List<string> Photos { get; set; } = new List<string>();

        public List<LinkedFile> PinnedFiles { get; set; } = new List<LinkedFile>();

        [Required]
        public bool New { get; set; }

        [Required]
        public bool InStock { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "TimesBought cannot be negative.")]
        public int TimesBought { get; set; }

        public List<Opinion> Opinions { get; set; } = new List<Opinion>();
    }
}
