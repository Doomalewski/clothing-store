using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Card
    {
        [Key]
        public int CardId { get; set; }

        [Required]
        [MaxLength(16)]
        [MinLength(13)]
        [RegularExpression(@"^\d{13,16}$", ErrorMessage = "Card number must be between 13 and 16 digits.")]
        public string Numbers { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }


        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [Range(100, 9999, ErrorMessage = "CVV code must be between 3 and 4 digits.")]
        public int CvvCode { get; set; }
    }

