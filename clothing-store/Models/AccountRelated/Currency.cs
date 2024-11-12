using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Currency
    {
        [Key]
        public int CurrencyId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Symbol { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal PlnToCurrRatio { get; set; }
    }
