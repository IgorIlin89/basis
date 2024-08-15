using ApiCouponProduct.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCouponProduct.Database;

public static class DbServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
    IConfiguration configuration)
    {
        services.AddDbContext<ApiCouponProductContext>(configure =>
        {
            configure.UseSqlServer(configuration.GetConnectionString("ApiOnlineShopWebDb"), b => b.MigrationsAssembly("ApiCouponProduct"));
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>().
            AddScoped<IProductRepository, ProductRepository>().
            AddScoped<ICouponRepository, CouponRepository>();

        return services;
    }
}
