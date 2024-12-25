using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;



namespace clothing_store.Models
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options) { }

        // AccountRelated
        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<UserEvent> UserEvents { get; set; }

        // OrderRelated
        public virtual DbSet<Card> Cards { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }


        // Product
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<LinkedFile> Files { get; set; }
        public virtual DbSet<Opinion> Opinions { get; set; }
        public virtual DbSet<SpecialDiscount> SpecialDiscounts { get; set; }
        public virtual DbSet<Tax> Taxes { get; set; }
        public virtual DbSet<BasketProduct> BasketProducts { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }
    }
}
