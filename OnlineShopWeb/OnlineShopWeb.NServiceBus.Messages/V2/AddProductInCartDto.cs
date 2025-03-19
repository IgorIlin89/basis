namespace OnlineShopWeb.Messages.V2;

public class AddProductInCartDto
{
    public int ProductId { get; set; }
    public int Count { get; set; }
    public decimal PricePerProduct { get; set; }
}
