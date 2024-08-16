namespace ApiTransactionHistory.Domain;

public class AddTransactionHistory
{
    public int? Id { get; set; }
    public int? TransactionHistoryToCouponsId { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal? FinalPrice { get; set; }
    public TransactionHistoryToCoupons? Coupons { get; set; }
    public ICollection<ProductInCart> ProductsInCart { get; set; }
}
