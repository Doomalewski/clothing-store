﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clothing_store.Models.Product;

namespace Sklep_Konsola.OrderRelated
{
    public class OrderProduct
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

}
