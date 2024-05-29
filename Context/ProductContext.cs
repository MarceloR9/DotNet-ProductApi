using Microsoft.EntityFrameworkCore;
using ProductApi.Models;

namespace ProductApi.Context
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) 
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.Preco)
                .HasColumnType("decimal(18,2)");
        }
    }
}
