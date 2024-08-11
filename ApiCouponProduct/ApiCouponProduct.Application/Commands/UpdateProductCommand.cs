using ApiCouponProduct.Domain;
using ApiCouponProduct.Domain.Dtos;
using ApiCouponProduct.Domain.Exceptions;

namespace ApiCouponProduct.Application.Commands;

public record UpdateProductCommand
{
    public Product ProductToUpdate { get; init; }
    public UpdateProductCommand(ProductDto productDto)
    {
        if (productDto is null || productDto.ProductId is null)
        {
            throw new NotFoundException($"Can not update product. Either you provided no id or the object is null");
        }

        ProductToUpdate = productDto.MapToProduct();
    }
}
