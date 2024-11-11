using Microsoft.EntityFrameworkCore;
using Sklep_Konsola.AccountRelated;
using Sklep_Konsola.OrderRelated;
using Sklep_Konsola;
using clothing_store.Models.Product;

namespace clothing_store.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        // AccountRelated
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Basket> Baskets { get; set; }

        // OrderRelated
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

        // Product
        public virtual DbSet<clothing_store.Models.Product.Product> Products { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<LinkedFile> Files { get; set; }
        public virtual DbSet<Opinion> Opinions { get; set; }
        public virtual DbSet<Price> Prices { get; set; }
        public virtual DbSet<SpecialDiscount> SpecialDiscounts { get; set; }
    }
}
