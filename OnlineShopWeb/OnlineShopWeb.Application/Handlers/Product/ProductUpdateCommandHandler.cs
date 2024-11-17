using OnlineShopWeb.Application.Commands.Product;
using OnlineShopWeb.Application.Interfaces;
using ProductCouponAdapter;
using ProductCouponAdapter.Mapping;

namespace OnlineShopWeb.Application.Handlers.Product;

public class ProductUpdateCommandHandler(IProductCouponAdapter productCouponAdapter) : IProductUpdateCommandHandler
{
    public async Task<Domain.Product> Handle(ProductUpdateCommand command)
    {
        var productToUpdate = new Domain.Product
        {
            Id = command.ProductId,
            Name = command.Name,
            Producer = command.Producer,
            Category = command.Category.MapToDomain(),
            Picture = command.Picture,
            Price = command.Price
        };

        var result = await productCouponAdapter.ProductUpdate(productToUpdate);
        return result;
    }
}
