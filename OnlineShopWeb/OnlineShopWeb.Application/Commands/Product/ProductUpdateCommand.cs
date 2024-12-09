using ProductCouponAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Product;

public record ProductUpdateCommand(int ProductId, string Name, string Producer,
        ProductCategoryDto Category, string Picture, decimal Price)
{
}
