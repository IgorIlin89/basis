using ApiCouponProduct.Domain;
using ApiCouponProduct.Domain.Dtos;

namespace ApiCouponProduct.Application.Commands;

public record UpdateProductCommand
{
    public Product ProductToUpdate { get; init; }
    public UpdateProductCommand(UpdateProductDto updateProductDto)
    {
        ProductToUpdate = updateProductDto.MapToProduct();
    }
}
