using ApiUser.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUser.Database;

public static class DbServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApiUserContext>(configure =>
        {
            configure.UseSqlServer(configuration.GetConnectionString("ApiOnlineShopWebDb"));
        });

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
