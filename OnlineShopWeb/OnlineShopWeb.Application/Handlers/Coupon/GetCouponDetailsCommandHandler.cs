using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using ProductCouponAdapter;

namespace OnlineShopWeb.Application.Handlers.Coupon;

public class GetCouponDetailsCommandHandler(IProductCouponAdapter productCouponAdapter) : IGetCouponDetailsCommandHandler
{
    public async Task<Domain.Coupon> Handle(GetCouponDetailsCommand command)
    {
        var result = await productCouponAdapter.GetCouponById(command.Id);
        return result;
    }
}
