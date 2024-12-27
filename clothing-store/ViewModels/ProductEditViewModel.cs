using clothing_store.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace clothing_store.ViewModels
{
    public class ProductEditViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int TaxId { get; set; }
        public int Quantity { get; set; }
        public bool Visible { get; set; }
        public bool New { get; set; }
        public bool InStock { get; set; }
        [ValidateNever]
        public IEnumerable<Brand> Brands { get; set; }
        [ValidateNever]
        public IEnumerable<dynamic> Taxes { get; set; } 
    }

}
