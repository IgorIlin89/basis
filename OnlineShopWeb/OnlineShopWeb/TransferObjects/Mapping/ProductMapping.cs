using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class ProductMapping
{
    public static ProductModel MapToModel(this ProductDto productDto)
    {
        var productModel = new ProductModel();

        if (productDto.ProductId is not null)
        {
            productModel.ProductId = productDto.ProductId;
        }

        productModel.Name = productDto.Name;
        productModel.Producer = productDto.Producer;
        productModel.Category = productDto.Category;
        productModel.Picture = productDto.Picture;
        productModel.Price = productDto.Price;

        return productModel;
    }
}
