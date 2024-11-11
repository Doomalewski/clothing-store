using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_Konsola.AccountRelated
{
    using System.ComponentModel.DataAnnotations;

    public class Address
    {
        [Key]
        [Required(ErrorMessage = "AddressId is required.")]
        public int AddressId { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [StringLength(100, ErrorMessage = "Street length can't exceed 100 characters.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City length can't exceed 100 characters.")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "State length can't exceed 100 characters.")]
        public string State { get; set; }

        [RegularExpression(@"^\d{2}-\d{3}$|^\d{5}$", ErrorMessage = "ZipCode must be in the format XX-XXX or XXXXX.")]
        public string ZipCode { get; set; }


        [Required(ErrorMessage = "Country is required.")]
        [StringLength(100, ErrorMessage = "Country length can't exceed 100 characters.")]
        public string Country { get; set; }


    }


}
