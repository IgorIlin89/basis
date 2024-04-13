using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopWeb.Database.Interfaces;

namespace OnlineShopWeb.Database;

public static class DbServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OnlineShopWebDbContext>(configure =>
        {
            configure.UseSqlServer(configuration.GetConnectionString("OnlineShopWebDb"));
        });

        services.AddScoped<IProductRepository, ProductRepository>()
        .AddScoped<ICouponRepository, CouponRepository>()
        .AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}