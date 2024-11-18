using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clothing_store.Models;

    public class Basket
    {
        [Key]
        [Required(ErrorMessage = "BasketId is required")]
        public int BasketId { get; set; }
        public List<ProductWithQuantity> Products { get; set; }
    }

