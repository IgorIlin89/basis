namespace OnlineShopWeb.Application.Commands.Coupon;

public record GetCouponDetailsCommand
{
    public int Id;

    public GetCouponDetailsCommand(int id)
    {
        Id = id;
    }
}
