using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class ProductMapping
{
    public static ProductModel MapToModel(this ProductDto productDto)
    {
        return new ProductModel
        {
            ProductId = productDto.ProductId,
            Name = productDto.Name,
            Producer = productDto.Producer,
            Category = productDto.Category,
            Picture = productDto.Picture,
            Price = productDto.Price,
        };
        
    }
}
