using Microsoft.VisualBasic.FileIO;
using Sklep_Konsola;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace clothing_store.Models.Product
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public Brand? Brand { get; set; }
        public string? Description { get; set; } 
        public Price Price { get; set; }
        public string? PhotoPath { get; set; }
        public int Views { get; set; }
        public bool Visible { get; set; }
        public List<string> Photos = new List<string>();
        public List<LinkedFile> PinnedFiles = new List<LinkedFile>();
        public bool New { get; set; }
        public bool InStock { get; set; }
        public int Quantity { get; set; }
        public int TimesBought { get; set; }
        public List<Opinion> Opinions { get; set; } = new List<Opinion>();

    }
}
