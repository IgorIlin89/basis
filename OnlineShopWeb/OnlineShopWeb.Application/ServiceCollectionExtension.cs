using Microsoft.Extensions.DependencyInjection;
using OnlineShopWeb.Application.Handlers.User;
using OnlineShopWeb.Application.Interfaces;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        //return serviceCollection;

        return serviceCollection.AddScoped<IGetUserByEmailCommandHandler, GetUserByEmailCommandHandler>().
            AddScoped<IUserAddCommandHandler, UserAddCommandHandler>();
    }
}