namespace ApiCouponProduct.Application.Commands;

public record GetCouponByIdCommand
{
    public int Id { get; init; }
    public GetCouponByIdCommand(int id)
    {
        Id = id;
    }
}
