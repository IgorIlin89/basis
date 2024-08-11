using ApiCouponProduct.Domain.Exceptions;

namespace ApiCouponProduct.Application.Commands;

public record DeleteProductCommand
{
    public int Id { get; init; }
    public DeleteProductCommand(string id)
    {
        if (id is null)
        {
            throw new NotFoundException($"Id may not be null when trying to delete a product");
        }

        Id = Int32.Parse(id);
    }
}
