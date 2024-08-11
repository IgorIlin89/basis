using ApiCouponProduct.Domain.Exceptions;

namespace ApiCouponProduct.Application.Commands;

public record GetProductByIdCommand
{
    public int Id { get; init; }

    public GetProductByIdCommand(string id)
    {
        if (id is null)
        {
            throw new NotFoundException($"The id may not be null when using GetProductById");
        }
        Id = Int32.Parse(id);
    }
}
