using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clothing_store.Models;
namespace clothing_store.Models
{
    public class Basket
    {
        [Key]
        [Required(ErrorMessage = "BasketId is required")]
        public int BasketId { get; set; }

        public int AccountId { get; set; }
        [ForeignKey(nameof(AccountId))]
        public Account Account { get; set; }
        public List<BasketProduct> BasketProducts { get; set; }
    }
}
