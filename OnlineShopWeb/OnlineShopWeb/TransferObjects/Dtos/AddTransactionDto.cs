namespace OnlineShopWeb.TransferObjects.Dtos;

public class AddTransactionDto
{
    public int UserId { get; init; }
    public decimal? FinalPrice { get; init; }
    public List<ProductInCartDto> ProductsInCart { get; init; }
    public List<AddTransactionToCouponsDto>? Coupons { get; init; }
}

