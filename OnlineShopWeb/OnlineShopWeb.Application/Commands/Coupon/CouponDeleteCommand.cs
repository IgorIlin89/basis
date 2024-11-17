namespace OnlineShopWeb.Application.Commands.Coupon;

public record CouponDeleteCommand
{
    public readonly string CouponId;

    public CouponDeleteCommand(string couponId)
    {
        CouponId = couponId;
    }
}
