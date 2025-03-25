using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;

namespace OnlineShopWeb.Application.Handlers.Coupon;

public class GetCouponByIdCommandHandler(IProductCouponAdapter productCouponAdapter) : IGetCouponByIdCommandHandler
{
    public async Task<Domain.Coupon> Handle(GetCouponByIdCommand command,
        CancellationToken cancellationToken)
    {
        var result = await productCouponAdapter.GetCouponById(command.CouponId);
        return result;
    }
}
