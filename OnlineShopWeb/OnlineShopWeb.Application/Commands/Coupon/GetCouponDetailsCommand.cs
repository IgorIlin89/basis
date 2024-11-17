namespace OnlineShopWeb.Application.Commands.Coupon;

public record GetCouponDetailsCommand
{
    public readonly string Id;

    public GetCouponDetailsCommand(string id)
    {
        Id = id;
    }
}
