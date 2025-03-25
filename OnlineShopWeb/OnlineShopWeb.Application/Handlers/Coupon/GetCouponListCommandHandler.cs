using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;

namespace OnlineShopWeb.Application.Handlers.Coupon;

public class GetCouponListCommandHandler(IProductCouponAdapter productCouponAdapter) : IGetCouponListCommandHandler
{
    public async Task<List<Domain.Coupon>> Handle()
    {
        var result = await productCouponAdapter.GetCouponList();
        return result;
    }
}
