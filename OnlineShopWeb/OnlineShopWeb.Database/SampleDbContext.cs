using Microsoft.EntityFrameworkCore;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database;

public class SampleDbContext : DbContext
{
	public SampleDbContext(DbContextOptions<SampleDbContext> options) :
		base(options)
	{
	}

	public DbSet<Customer> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
		modelBuilder.Entity<Customer>().ToTable("Customer");
    }
}

