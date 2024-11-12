using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class ShippingMethod
{
    [Key]
    public int ShippingMethodId { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public float Price { get; set; }

    [StringLength(200, ErrorMessage = "Estimated delivery time cannot be longer than 200 characters.")]
    public string EstimatedDeliveryTime { get; set; }
}
