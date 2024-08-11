using ApiUser.Application.Handlers;
using ApiUser.Application.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiUser.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection.
            AddScoped<IGetUserListCommandHandler, GetUserListCommandHandler>().
            AddScoped<IGetUserByEmailCommandHandler, GetUserByEmailCommandHandler>().
            AddScoped<IGetUserByIdCommandHandler, GetUserByIdCommandHandler>().
            AddScoped<IUpdateUserCommandHandler, UpdateUserCommandHandler>().
            AddScoped<IDeleteUserCommandHandler, DeleteUserCommandHandler>().
            AddScoped<IAddUserCommandHandler, AddUserCommandHandler>().
            AddScoped<IChangePasswordCommandHandler, ChangePasswordCommandHandler>();
    }
}
