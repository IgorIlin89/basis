namespace OnlineShopWeb.Application.Commands;

public record GetCouponDetailsCommand
{
    public int Id;

    public GetCouponDetailsCommand(int id)
    {
        Id = id;
    }
}
