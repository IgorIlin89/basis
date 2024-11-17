namespace OnlineShopWeb.Application.Commands.Product;

public record ProductDeleteCommand
{
    public readonly string ProductId;

    public ProductDeleteCommand(string productId)
    {
        ProductId = productId;
    }
}
