﻿using ApiOnlineShopWeb.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OnlineShopWeb.Database;

public static class DbServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApiOnlineShopWebContext>(configure =>
        {
            configure.UseSqlServer(configuration.GetConnectionString("OnlineShopWebDb"), b => b.MigrationsAssembly("OnlineShopWeb"));
        });

        //services.AddScoped<IProductRepository, ProductRepository>()
        //.AddScoped<ICouponRepository, CouponRepository>()
        //.AddScoped<IUserRepository, UserRepository>()
        //.AddScoped<ITransactionHistoryRepository, TransactionHistoryRepository>();
        //.AddScoped<IShoppingCartRepository, ShoppingCartRepository>()

        return services;
    }
}