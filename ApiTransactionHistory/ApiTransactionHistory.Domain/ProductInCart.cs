namespace ApiTransactionHistory.Domain;

public class ProductInCart
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public decimal PricePerProduct { get; set; }
    public int TransactionHistoryId { get; set; }
}
