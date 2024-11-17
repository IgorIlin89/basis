namespace OnlineShopWeb.Application.Commands.Coupon;

public record GetCouponByIdCommand
{
    public readonly string CouponId;

    public GetCouponByIdCommand(string couponId)
    {
        CouponId = couponId;
    }
}
