using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ApiUser.Application.Handlers;
using ApiUser.Application.Handlers.Interfaces;

namespace ApiUser.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection.
            AddScoped<IGetUserListCommandHandler, GetUserListCommandHandler>().
            AddScoped<IGetUserByEmailCommandHandler, GetUserByEmailCommandHandler>();
    }
}
