using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopWeb.Domain.Interfaces;
using Utility.Misc.Options;

namespace UserAdapter;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureUserAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = new ApiUserOptions();
        configuration.GetSection("ApiUserClientOptions").Bind(config);
        services.AddSingleton<ApiUserOptions>(config);

        return services.AddScoped<IUserAdapter, UserAdapter>();
    }
}
