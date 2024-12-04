using clothing_store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public enum DiscountType
{
    Percentage,
    FixedAmount
}
public class SpecialDiscount
{
    [Key]
    public int SpecialDiscountId { get; set; }

    [Required(ErrorMessage = "Code is reqiured")]
    public string Code { get; set; }

    [Required(ErrorMessage = "End time is required.")]
    public DateTime EndTime { get; set; }
    [Required]
    public int AccountId { get; set; }
    [ForeignKey(nameof(AccountId))]
    [Required]
    Account Account { get; set; }

    [Range(0, 100, ErrorMessage = "Discount amount must be between 0 and 100.")]
    public int DiscountAmount { get; set; } 
    [Required]
    public DiscountType Type { get; set; }
}
