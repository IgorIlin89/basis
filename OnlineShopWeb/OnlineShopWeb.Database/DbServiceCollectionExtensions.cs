using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShopWeb.Database;

public static class DbServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<SampleDbContext>(configure =>
        {
            configure.UseSqlServer(configuration.GetConnectionString("SampleDb"));
        });

        services.AddScoped<ISampleRepository, SampleRepository>();

        return services;
    }
}