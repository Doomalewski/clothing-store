using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clothing_store.Models.Product;
using Sklep_Konsola.OrderRelated;

namespace Sklep_Konsola.AccountRelated
{
    public class Basket
    {
        public List<OrderProduct> Products { get; set; }
    }

}
