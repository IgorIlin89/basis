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
    public DbSet<ShoppingCart> ShoppingCart { get; set; }
    public DbSet<TransactionHistory> TransactionHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionHistory>()
            .HasMany(o => o.Coupons)
            .WithMany(o => o.TransactionHistories)
            .UsingEntity(o => o.ToTable("TransactionHistoryToCouponsJoinTable"));
    }
}

