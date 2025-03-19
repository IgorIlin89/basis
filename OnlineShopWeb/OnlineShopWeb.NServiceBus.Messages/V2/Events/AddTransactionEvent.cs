namespace OnlineShopWeb.Messages.V2.Events;

public class AddTransactionEvent : IEvent
{
    public int UserId { get; set; }
    public IReadOnlyCollection<AddProductInCartDto> AddProductsInCartDto { get; set; }
    public IReadOnlyCollection<TransactionCouponDto> AddCouponsDto { get; set; }
}
