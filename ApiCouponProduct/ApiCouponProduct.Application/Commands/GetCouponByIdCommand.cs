using ApiCouponProduct.Domain.Exceptions;


namespace ApiCouponProduct.Application.Commands;

public record GetCouponByIdCommand
{
    public int Id { get; init; }
    public GetCouponByIdCommand(string id)
    {
        if (id is null)
        {
            throw new NotFoundException($"Id may not be null when trying to use GetCouponById");
        }

        Id = Int32.Parse(id);
    }
}
