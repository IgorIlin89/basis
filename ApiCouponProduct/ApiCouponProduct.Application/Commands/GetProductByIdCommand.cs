namespace ApiCouponProduct.Application.Commands;

public record GetProductByIdCommand
{
    public int Id { get; init; }

    public GetProductByIdCommand(int id)
    {
        Id = id;
    }
}
