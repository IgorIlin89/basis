namespace OnlineShopWeb.Application.Commands.Coupon;

public record GetCouponByCodeCommand
{
    public readonly string CouponCode;

    public GetCouponByCodeCommand(string couponCode)
    {
        CouponCode = couponCode;
    }
}
