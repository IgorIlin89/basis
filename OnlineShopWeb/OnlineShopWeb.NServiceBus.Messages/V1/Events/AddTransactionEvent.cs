namespace OnlineShopWeb.Messages.V1.Events;

public class AddTransactionEvent : IEvent
{
    public int UserId { get; set; }
    public DateTimeOffset PaymentDate { get; init; }
    public List<AddProductInCartDto> AddProductsInCartDto { get; set; }
    public List<AddTransactionToCouponsDto>? AddCouponsDto { get; set; }
}
