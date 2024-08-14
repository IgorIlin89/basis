namespace ApiTransactionHistory.Domain.Dtos;

public class AddTransactionHistoryDto
{
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
    public int UserId { get; set; }
    public TransactionHistoryToCouponsDto? Coupons { get; set; }
    public ICollection<ProductInCartDto> ProductsInCart { get; set; }
}
