using clothing_store;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SpecialDiscount
{
    [Key]
    public int SpecialDiscountId { get; set; }

    [Required(ErrorMessage = "Start time is required.")]
    public DateTime StartTime { get; set; }

    [Required(ErrorMessage = "End time is required.")]
    [GreaterThan("StartTime", ErrorMessage = "End time must be later than start time.")]
    public DateTime EndTime { get; set; }

    [Range(0, 100, ErrorMessage = "Discount amount must be between 0 and 100.")]
    public int DiscountAmount { get; set; }

    public SpecialDiscount(DateTime startTime, DateTime endTime, int discountAmount)
    {
        StartTime = startTime;
        EndTime = endTime;
        DiscountAmount = discountAmount;
    }
}
