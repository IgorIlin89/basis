using ApiTransactionHistory.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTransactionHistory.Database;

public static class DbServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApiTransactionHistoryContext>(configure =>
        {
            configure.UseSqlServer(configuration.GetConnectionString("ApiOnlineShopWebDb"), b => b.MigrationsAssembly("ApiTransactionHistory"));
        });

        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IApiTransactionHistoryRepository, ApiTransactionHistoryRepository>();

        return serviceCollection;
    }
}
