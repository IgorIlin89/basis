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
    public DbSet<User> User { get; set; }
    public DbSet<Coupon> Coupon { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<User>().ToTable("User");
        modelBuilder.Entity<Coupon>().ToTable("Coupon").HasKey( o => o.Id);
        //modelBuilder.Entity<Coupon>().ToTable("Coupon").HasKey( o => o.Id);
    }
}

