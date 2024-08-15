using ApiCouponProduct.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiCouponProduct.Database;

public class ApiCouponProductContext : DbContext
{
    public ApiCouponProductContext(DbContextOptions<ApiCouponProductContext> options)
        : base(options)
    {

    }

    public DbSet<Coupon> Coupon { get; set; }
    public DbSet<Product> Product { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(o => o.Price)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<Product>()
            .HasData(new Domain.Product
            {
                Id = 1,
                Name = "Persil",
                Producer = "Henkel",
                Category = ProductCategory.Cleaning,
                Picture = "persil.jpg",
                Price = 5.99m
            });

        modelBuilder.Entity<Product>()
            .HasData(new Domain.Product
            {
                Id = 2,
                Name = "Inkpad 4",
                Producer = "Pocketbook",
                Category = ProductCategory.Electronics,
                Picture = "inkpad4.jpg",
                Price = 239.99m
            });

        modelBuilder.Entity<Product>()
            .HasData(new Domain.Product
            {
                Id = 3,
                Name = "Giotto",
                Producer = "Ferrero",
                Category = ProductCategory.Sweets,
                Picture = "giotto.jpg",
                Price = 2.99m
            });

        modelBuilder.Entity<Product>()
            .HasData(new Domain.Product
            {
                Id = 4,
                Name = "Reis",
                Producer = "Bioland",
                Category = ProductCategory.Food,
                Picture = "reis.jpg",
                Price = 0.99m
            });

        modelBuilder.Entity<Coupon>()
            .HasData(new Domain.Coupon
            {
                Id = 1,
                Code = "TestInRange",
                AmountOfDiscount = 111,
                TypeOfDiscount = TypeOfDiscount.Total,
                MaxNumberOfUses = 323,
                StartDate = new DateTimeOffset(2022, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
                EndDate = new DateTimeOffset(2026, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
            });

        modelBuilder.Entity<Coupon>()
            .HasData(new Domain.Coupon
            {
                Id = 2,
                Code = "TestOutOfRange",
                AmountOfDiscount = 22,
                TypeOfDiscount = TypeOfDiscount.Total,
                MaxNumberOfUses = 32,
                StartDate = new DateTimeOffset(2022, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
                EndDate = new DateTimeOffset(2020, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
            });

        modelBuilder.Entity<Coupon>()
            .HasData(new Domain.Coupon
            {
                Id = 3,
                Code = "Test",
                AmountOfDiscount = 25,
                TypeOfDiscount = TypeOfDiscount.Percentage,
                MaxNumberOfUses = 670,
                StartDate = new DateTimeOffset(2022, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
                EndDate = new DateTimeOffset(2026, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
            });

        modelBuilder.Entity<Coupon>()
            .HasData(new Domain.Coupon
            {
                Id = 4,
                Code = "Testing",
                AmountOfDiscount = 50,
                TypeOfDiscount = TypeOfDiscount.Percentage,
                MaxNumberOfUses = 554,
                StartDate = new DateTimeOffset(2022, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
                EndDate = new DateTimeOffset(2026, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
            });

        modelBuilder.Entity<Coupon>()
            .HasData(new Domain.Coupon
            {
                Id = 5,
                Code = "TestMaxNumberOfUses",
                AmountOfDiscount = 75,
                TypeOfDiscount = TypeOfDiscount.Percentage,
                MaxNumberOfUses = 0,
                StartDate = new DateTimeOffset(2022, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
                EndDate = new DateTimeOffset(2026, 01, 01, 12, 12, 12, TimeSpan.FromHours(7)),
            });
    }
}
