using ProductCouponAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Product;

public record ProductAddCommand
{
    public readonly string ProductId;
    public readonly string Name;
    public readonly string Producer;
    public readonly ProductCategoryDto Category;
    public readonly string Picture;
    public readonly decimal Price;

    public ProductAddCommand(string productId, string name, string producer,
    ProductCategoryDto category, string picture, decimal price)
    {
        ProductId = productId;
        Name = name;
        Producer = producer;
        Category = category;
        Picture = picture;
        Price = price;
    }
}
