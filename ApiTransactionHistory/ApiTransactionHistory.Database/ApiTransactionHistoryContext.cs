using ApiTransactionHistory.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiTransactionHistory.Database;

public class ApiTransactionHistoryContext : DbContext
{
    public ApiTransactionHistoryContext(DbContextOptions<ApiTransactionHistoryContext> context)
        : base(context)
    {

    }

    public DbSet<TransactionHistory> TransactionHistory { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductInCart>()
            .Property(o => o.PricePerProduct)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<TransactionHistory>()
            .HasMany(o => o.ProductsInCart)
            .WithOne()
            .HasForeignKey(o => o.TransactionHistoryId)
            .IsRequired();

        modelBuilder.Entity<TransactionHistory>()
            .HasOne(o => o.Coupons)
            .WithMany()
            .HasForeignKey(o => o.TransactionHistoryToCouponsId);
    }
}
