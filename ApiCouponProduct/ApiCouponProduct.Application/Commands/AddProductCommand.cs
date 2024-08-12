using ApiCouponProduct.Domain;
using ApiCouponProduct.Domain.Dtos;
using ApiCouponProduct.Domain.Exceptions;

namespace ApiCouponProduct.Application.Commands;

public record AddProductCommand
{
    public Product ProductToAdd { get; init; }

    public AddProductCommand(AddProductDto productDto)
    {
        if (productDto is null)
        {
            throw new NotFoundException($"Can not add null as a new product");
        }

        ProductToAdd = productDto.MapToProduct();
    }
}
