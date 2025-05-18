using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;

namespace OnlineShopWeb.Application.Handlers.Coupon;

public class CouponDeleteCommandHandler(IProductCouponAdapter productCouponAdapter) : ICouponDeleteCommandHandler
{
    public void Handle(CouponDeleteCommand command)
    {
        productCouponAdapter.CouponDelete(command.Code);
    }
}
