using Microsoft.Extensions.DependencyInjection;

namespace ApiTransactionHistory.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection;
    }
}
