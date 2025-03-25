using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;

namespace OnlineShopWeb.Application.Handlers.Coupon;

public class GetCouponByCodeCommandHandler(IProductCouponAdapter productCouponAdapter) : IGetCouponByCodeCommandHandler
{
    public async Task<Domain.Coupon> Handle(GetCouponByCodeCommand command)
    {
        var result = await productCouponAdapter.GetCouponByCode(command.CouponCode);
        return result;
    }
}
