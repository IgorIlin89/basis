using OnlineShopWeb.Application.Commands.Product;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;
using ProductCouponAdapter.Mapping;

namespace OnlineShopWeb.Application.Handlers.Product;

public class ProductAddCommandHandler(IProductCouponAdapter productCouponAdapter) : IProductAddCommandHandler
{
    public async Task<Domain.Product> Handle(ProductAddCommand command)
    {
        var productToAdd = new Domain.Product
        {
            Name = command.Name,
            Producer = command.Producer,
            Category = command.Category.MapToDomain(),
            Picture = command.Picture,
            Price = command.Price
        };

        var result = await productCouponAdapter.ProductAdd(productToAdd);
        return result;
    }
}
