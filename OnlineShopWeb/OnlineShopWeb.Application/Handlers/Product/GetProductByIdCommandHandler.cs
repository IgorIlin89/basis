using OnlineShopWeb.Application.Commands.Product;
using OnlineShopWeb.Application.Interfaces;
using ProductCouponAdapter;

namespace OnlineShopWeb.Application.Handlers.Product;

public class GetProductByIdCommandHandler(IProductCouponAdapter productCouponAdapter) : IGetProductByIdCommandHandler
{
    public async Task<Domain.Product> Handle(GetProductByIdCommand command)
    {
        var result = await productCouponAdapter.GetProductById(command.ProductId);
        return result;
    }
}
