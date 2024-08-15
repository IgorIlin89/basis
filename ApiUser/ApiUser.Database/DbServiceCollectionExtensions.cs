using ApiUser.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiUser.Database;

public static class DbServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApiUserContext>(configure =>
        {
            configure.UseSqlServer(configuration.GetConnectionString("ApiOnlineShopWebDb"), b => b.MigrationsAssembly("ApiUser"));
        });

        services.AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
