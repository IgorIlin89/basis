using ApiCouponProduct.Domain.Exceptions;

namespace ApiCouponProduct.Application.Commands;

public record DeleteCouponCommand
{
    public int Id { get; init; }

    public DeleteCouponCommand(string id)
    {
        if (id is null)
        {
            throw new NotFoundException($"Id may not be null when trying to delete a coupon");
        }

        Id = Int32.Parse(id);
    }
}
