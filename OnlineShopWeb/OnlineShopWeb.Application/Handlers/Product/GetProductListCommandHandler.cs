using OnlineShopWeb.Application.Commands.Product;
using OnlineShopWeb.Application.Interfaces;
using ProductCouponAdapter;

namespace OnlineShopWeb.Application.Handlers.Product;

public class GetProductListCommandHandler(IProductCouponAdapter productCouponAdapter) : IGetProductListCommandHandler
{
    public async Task<List<Domain.Product>> Handle(GetProductListCommand command)
    {
        var result = await productCouponAdapter.GetProductList();
        return result;
    }
}
