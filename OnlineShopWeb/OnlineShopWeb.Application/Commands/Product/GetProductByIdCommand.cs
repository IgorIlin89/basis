namespace OnlineShopWeb.Application.Commands.Product;

public record GetProductByIdCommand
{
    public readonly string ProductId;

    public GetProductByIdCommand(string productId)
    {
        ProductId = productId;
    }
}
