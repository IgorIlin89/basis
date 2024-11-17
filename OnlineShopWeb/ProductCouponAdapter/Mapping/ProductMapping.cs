using OnlineShopWeb.Domain;
using ProductCouponAdapter.DTOs;

namespace ProductCouponAdapter.Mapping;

public static class ProductMapping
{
    public static Product MapToProduct(this ProductDto productDto) =>
        new Product
        {
            Id = productDto.ProductId is null ? 0 : productDto.ProductId.Value,
            Name = productDto.Name,
            Producer = productDto.Producer,
            Category = (ProductCategory)productDto.Category,
            Picture = productDto.Picture,
            Price = productDto.Price
        };

    public static ProductDto MapToDto(this Product product) =>
        new ProductDto
        {
            ProductId = product.Id,
            Name = product.Name,
            Producer = product.Producer,
            Category = (ProductCategoryDto)product.Category,
            Picture = product.Picture,
            Price = product.Price
        };

    public static ProductCategory MapToDomain(this ProductCategoryDto dto) =>
        dto switch
        {
            ProductCategoryDto.Cleaning => ProductCategory.Cleaning,
            ProductCategoryDto.Sweets => ProductCategory.Sweets,
            ProductCategoryDto.Food => ProductCategory.Food,
            ProductCategoryDto.Electronics => ProductCategory.Electronics,
            _ => throw new NotImplementedException()
        };

    public static List<Product> MapToList(this List<ProductDto> productDtoList) =>
        productDtoList.Select(o => o.MapToProduct()).ToList();

}