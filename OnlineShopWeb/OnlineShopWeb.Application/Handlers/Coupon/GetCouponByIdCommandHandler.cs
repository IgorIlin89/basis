using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using ProductCouponAdapter;

namespace OnlineShopWeb.Application.Handlers.Coupon;

public class GetCouponByIdCommandHandler(IProductCouponAdapter productCouponAdapter) : IGetCouponByIdCommandHandler
{
    public async Task<Domain.Coupon> Handle(GetCouponByIdCommand command)
    {
        var result = await productCouponAdapter.GetCouponById(command.CouponId);
        return result;
    }
}
