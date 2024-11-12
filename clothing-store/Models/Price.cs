using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
[NotMapped]
public class Price
{
    [Range(0, float.MaxValue, ErrorMessage = "Full price must be a positive value.")]
    public float FullPrice { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "Discount price must be a positive value, or null if no discount.")]
    public float? DiscountPrice { get; set; }

    [Required(ErrorMessage = "VAT tax is required.")]
    public Tax Vat { get; set; }

    [Range(0, float.MaxValue, ErrorMessage = "Netto price must be a positive value.")]
    public float NettoPrice { get; set; }

    public Price(float fullPrice, float? discountPrice, Tax vat)
    {
        FullPrice = fullPrice;
        DiscountPrice = discountPrice;
        Vat = vat;
        NettoPrice = discountPrice.HasValue ? (fullPrice - discountPrice.Value) * (1 + vat.Value / 100f) : fullPrice * (1 + vat.Value / 100f);
    }
}
