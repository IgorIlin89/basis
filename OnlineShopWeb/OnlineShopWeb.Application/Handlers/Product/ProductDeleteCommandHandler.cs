using OnlineShopWeb.Application.Commands.Product;
using OnlineShopWeb.Application.Interfaces;
using ProductCouponAdapter;

namespace OnlineShopWeb.Application.Handlers.Product;

public class ProductDeleteCommandHandler(IProductCouponAdapter productCouponAdapter) : IProductDeleteCommandHandler
{
    public void Handle(ProductDeleteCommand command)
    {
        productCouponAdapter.ProductDelete(command.ProductId);
    }
}
