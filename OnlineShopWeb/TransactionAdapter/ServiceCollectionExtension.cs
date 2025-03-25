using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopWeb.Domain.Interfaces;
using Utility.Misc.Options;

namespace TransactionAdapter;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureTransactionAdapter(this IServiceCollection services,
    IConfiguration configuration)
    {
        var config = new ApiTransactionOptions();
        configuration.GetSection("ApiTransaction").Bind(config);
        services.AddSingleton<ApiTransactionOptions>(config);

        return services.AddScoped<ITransactionAdapter, TransactionAdapter>();
    }
}
