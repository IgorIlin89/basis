using Microsoft.EntityFrameworkCore;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database;

public class OnlineShopWebDbContext : DbContext
{
    public OnlineShopWebDbContext(DbContextOptions<OnlineShopWebDbContext> options) :
        base(options)
    {
    }

    public DbSet<Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Product");
    }
}

