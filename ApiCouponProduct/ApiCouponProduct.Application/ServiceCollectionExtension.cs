using ApiCouponProduct.Application.Handlers;
using ApiCouponProduct.Application.Handlers.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ApiCouponProduct.Application;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IGetProductListCommandHandler, GetProductListCommandHandler>().
            AddScoped<IGetCouponListCommandHandler, GetCouponListCommandHandler>().
            AddScoped<IGetProductByIdCommandHandler, GetProductByIdCommandHandler>().
            AddScoped<IDeleteProductByIdCommandHandler, DeleteProductByIdCommandHandler>().
            AddScoped<IUpdateProductCommandHandler, UpdateProductCommandHandler>().
            AddScoped<IAddProductCommandHandler, AddProductCommandHandler>().
            AddScoped<IGetCouponByIdCommandHandler, GetCouponByIdCommandHandler>().
            AddScoped<IGetCouponByCodeCommandHandler, GetCouponByCodeCommandHandler>().
            AddScoped<IDeleteCouponCommandHandler, DeleteCouponCommandHandler>().
            AddScoped<IUpdateCouponCommandHandler, UpdateCouponCommandHandler>().
            AddScoped<IAddCouponCommandHandler, AddCouponCommandHandler>();
    }
}
