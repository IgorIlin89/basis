namespace OnlineShopWeb.Messages.V1.Events;

public class AddTransactionEvent : IEvent
{
    public int UserId { get; set; }
    public List<AddProductInCartDto> AddProductsInCartDto { get; set; }
    public List<AddTransactionToCouponsDto>? AddCouponsDto { get; set; }
}
