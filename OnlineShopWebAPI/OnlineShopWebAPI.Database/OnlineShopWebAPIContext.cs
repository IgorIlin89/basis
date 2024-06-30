using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineShopWebAPI.Domain;

namespace OnlineShopWebAPI.Database;

public class OnlineShopWebAPIContext : DbContext
{
    public OnlineShopWebAPIContext(DbContextOptions<OnlineShopWebAPIContext> options)
        : base(options)
    {

    }

    public DbSet<User> User { get; set; } = null;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasData(new Domain.User
            {
                Id = -1,
                EMail = "igor@gmail.com",
                Password = "123456",
                Name = "Igor Il"
            });

        modelBuilder.Entity<User>()
            .HasData(new Domain.User
            {
                Id = -2,
                EMail = "yury@gmail.com",
                Password = "123456",
                Name = "Yury Spi"
            });
    }
}
