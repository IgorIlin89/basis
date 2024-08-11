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
}
