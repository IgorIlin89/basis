using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnlineShopWeb.Domain.Interfaces;
using Utility.Misc.Options;

namespace ProductCouponAdapter;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureProductCouponAdapter(this IServiceCollection services,
        IConfiguration configuration)
    {
        var config = new ApiCouponProductOptions();
        configuration.GetSection("ApiCouponProductClientOptions").Bind(config);
        services.AddSingleton<ApiCouponProductOptions>(config);

        return services.AddScoped<IProductCouponAdapter, ProductCouponAdapter>();
    }
}
