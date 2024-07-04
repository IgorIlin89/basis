using Microsoft.EntityFrameworkCore;
using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Database;

public class ApiOnlineShopWebContext : DbContext
{
    public ApiOnlineShopWebContext(DbContextOptions<ApiOnlineShopWebContext> options)
        : base(options)
    {

    }

    public DbSet<User> User { get; set; } = null;
    public DbSet<Product> Product { get; set; } = null;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(new Domain.User
            {
                Id = 1,
                EMail = "igor@gmail.com",
                Password = "123456",
                Name = "Igor Il"
            });

        modelBuilder.Entity<User>()
            .HasData(new Domain.User
            {
                Id = 2,
                EMail = "yury@gmail.com",
                Password = "123456",
                Name = "Yury Spi"
            });

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
    }
}
