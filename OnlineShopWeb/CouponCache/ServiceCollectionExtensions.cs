using Microsoft.Extensions.DependencyInjection;

namespace CouponCache;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCache(this IServiceCollection services)
    {
        services.AddSingleton<ICache, Cache>();

        return services;
    }
}
