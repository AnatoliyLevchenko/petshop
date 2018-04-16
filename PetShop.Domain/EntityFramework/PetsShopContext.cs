using System.Data.Entity;
using PetShop.Domain.Entities;

namespace PetShop.Domain.EntityFramework
{

    public class PetsShopContext : DbContext
    {
        public PetsShopContext() : base("name = DefaultConnection")
        {
            
        }
        public PetsShopContext(string stringConnection = "name=DefaultConnection")
            : base(stringConnection)
        {
        }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.PricePerItem)
                .HasPrecision(7, 2);

            modelBuilder.Entity<Product>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Order)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);
        }
    }
}