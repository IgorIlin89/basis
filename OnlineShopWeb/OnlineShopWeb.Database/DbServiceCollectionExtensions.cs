using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

        services.AddScoped<IProductRepository, ProductRepository>();

        return services;
    }
}