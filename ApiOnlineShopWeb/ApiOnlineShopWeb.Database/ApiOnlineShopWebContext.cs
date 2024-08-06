using Microsoft.EntityFrameworkCore;
using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Database;

public class ApiOnlineShopWebContext : DbContext
{
    public ApiOnlineShopWebContext(DbContextOptions<ApiOnlineShopWebContext> options)
        : base(options)
    {

    }

    public DbSet<Product> Product { get; set; } = null;
    public DbSet<User> User { get; set; } = null;
    public DbSet<Coupon> Coupon { get; set; } = null;
    public DbSet<TransactionHistory> TransactionHistory { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TransactionHistory>()
            .HasMany(o => o.Coupons)
            .WithMany(o => o.TransactionHistories)
            .UsingEntity(o => o.ToTable("TransactionHistoryToCoupons"));

        modelBuilder.Entity<TransactionHistory>()
            .HasOne(o => o.User)
            .WithMany(o => o.TransactionHistories)
            .HasForeignKey(o => o.UserId)
            .IsRequired();

        modelBuilder.Entity<ProductInCart>()
            .HasOne(o => o.Product)
            .WithMany(o => o.CartProduct)
            .HasForeignKey(o => o.ProductId)
            .IsRequired();

        modelBuilder.Entity<ProductInCart>()
            .HasOne(o => o.TransactionHistory)
            .WithMany(o => o.ProductsInCart)
            .HasForeignKey(o => o.TransactionHistoryId)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .Property(o => o.Price)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<TransactionHistory>()
            .Property(o => o.FinalPrice)
            .HasColumnType("decimal(10,2)");

        modelBuilder.Entity<User>()
            .HasData(new Domain.User
            {
                Id = 1,
                EMail = "igor@gmail.com",
                GivenName = "Igor",
                Surname = "Il",
                Age = 34,
                Country = "Germany",
                City = "Hamburg",
                Street = "Berner Chaussee",
                HouseNumber = 154,
                PostalCode = 22526,
                Password = "123456"
            });

        modelBuilder.Entity<User>()
            .HasData(new Domain.User
            {
                Id = 2,
                EMail = "yury@gmail.com",
                GivenName = "Yury",
                Surname = "Spi",
                Age = 38,
                Country = "Germany",
                City = "Harburg",
                Street = "Harburger Chaussee",
                HouseNumber = 22,
                PostalCode = 22041,
                Password = "123456"
            });

        modelBuilder.Entity<User>()
            .HasData(new Domain.User
            {
                Id = 3,
                EMail = "dirk@gmail.com",
                GivenName = "Dirk",
                Surname = "Es",
                Age = 33,
                Country = "Germany",
                City = "Berlin",
                Street = "Berliner Straße",
                HouseNumber = 232,
                PostalCode = 25014,
                Password = "123456"
            });

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
