namespace ApiCouponProduct.Domain.Dtos;

public static class MappingProduct
{
    public static AddProductDto MapToDto(this Product product)
    {
        return new AddProductDto
        {
            ProductId = product.Id,
            Name = product.Name,
            Producer = product.Producer,
            Category = product.Category,
            Picture = product.Picture,
            Price = product.Price
        };
    }
    public static Product MapToProduct(this AddProductDto addProductDto)
    {
        var product = new Product();

        product.Name = addProductDto.Name;
        product.Producer = addProductDto.Producer;
        product.Category = addProductDto.Category;
        product.Picture = addProductDto.Picture;
        product.Price = addProductDto.Price;

        return product;
    }
    public static Product MapToProduct(this UpdateProductDto updateProductDto)
    {
        var product = new Product();

        product.Id = updateProductDto.ProductId;
        product.Name = updateProductDto.Name;
        product.Producer = updateProductDto.Producer;
        product.Category = updateProductDto.Category;
        product.Picture = updateProductDto.Picture;
        product.Price = updateProductDto.Price;

        return product;
    }
    public static List<AddProductDto> MapToDtoList(this List<Product> productList)
    {
        var productDtoList = new List<AddProductDto>();

        foreach (var element in productList)
        {
            productDtoList.Add(element.MapToDto());
        }

        return productDtoList;
    }
}
