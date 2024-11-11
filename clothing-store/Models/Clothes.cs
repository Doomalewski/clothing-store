using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using clothing_store.Models.Product;

namespace Sklep_Konsola
{
    public class Tops : Product
    {
        public TopsCategory TopsType { get; set; }
    }

    public class Bottoms : Product
    {
        public BottomsCategory BottomsType { get; set; }
    }

    public class Shoes : Product
    {
        public ShoesCategory ShoesType { get; set; }
    }

    public class Accessory : Product
    {
        public AccessoryCategory AccessoryType { get; set; }
    }

}
