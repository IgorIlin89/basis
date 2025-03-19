using Microsoft.Extensions.DependencyInjection;
using OnlineShopWeb.Application.Handlers.Coupon;
using OnlineShopWeb.Application.Handlers.Product;
using OnlineShopWeb.Application.Handlers.Transaction;
using OnlineShopWeb.Application.Handlers.User;
using OnlineShopWeb.Application.Interfaces;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IGetUserByEmailCommandHandler, GetUserByEmailCommandHandler>().
            AddScoped<IUserAddCommandHandler, UserAddCommandHandler>().
            AddScoped<IChangeUserPasswordCommandHandler, ChangeUserPasswordCommandHandler>().
            AddScoped<IGetUserByIdCommandHandler, GetUserByIdCommandHandler>().
            AddScoped<IUserDeleteCommandHandler, UserDeleteCommandHandler>().
            AddScoped<IUserUpdateCommandHandler, UserUpdateCommandHandler>().
            AddScoped<IGetUserListCommandHandler, GetUserListCommandHandler>().
            AddScoped<ICouponAddCommandHandler, CouponAddCommandHandler>().
            AddScoped<ICouponDeleteCommandHandler, CouponDeleteCommandHandler>().
            AddScoped<IGetCouponDetailsCommandHandler, GetCouponDetailsCommandHandler>().
            AddScoped<ICouponUpdateCommandHandler, CouponUpdateCommandHandler>().
            AddScoped<IGetCouponByCodeCommandHandler, GetCouponByCodeCommandHandler>().
            AddScoped<IGetCouponListCommandHandler, GetCouponListCommandHandler>().
            AddScoped<IGetCouponByIdCommandHandler, GetCouponByIdCommandHandler>().
            AddScoped<IGetProductByIdCommandHandler, GetProductByIdCommandHandler>().
            AddScoped<IGetProductListCommandHandler, GetProductListCommandHandler>().
            AddScoped<IProductAddCommandHandler, ProductAddCommandHandler>().
            AddScoped<IProductDeleteCommandHandler, ProductDeleteCommandHandler>().
            AddScoped<IProductUpdateCommandHandler, ProductUpdateCommandHandler>().
            AddScoped<IAddTransactionCommandHandler, AddTransactionHttpCommandHandler>().
            AddScoped<IGetTransactionListCommandHandler, GetTransactionListCommandHandler>().
            AddScoped<IAddTransactionMessagesCommandHandler, AddTransactionMessagesCommandHandler>().
            AddScoped<IAddTransactionGrpcCommandHandler, AddTransactionGrpcCommandHandler>();
    }
}