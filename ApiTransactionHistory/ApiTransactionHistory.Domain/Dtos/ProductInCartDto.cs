namespace ApiTransactionHistory.Domain.Dtos;

public class ProductInCartDto
{
    public int Id { get; set; }
    public int Count { get; set; }
    public int ProductId { get; set; }
    public decimal PricePerProduct { get; set; }
    public int TransactionHistoryId { get; set; }
}
