using Microsoft.Extensions.DependencyInjection;
using OnlineShopWeb.Application.Handlers;
using OnlineShopWeb.Application.Interfaces;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection;

        return serviceCollection.
            AddScoped<IGetUserByEmailCommandHandler, GetUserByEmailCommandHandler>();
    }
}