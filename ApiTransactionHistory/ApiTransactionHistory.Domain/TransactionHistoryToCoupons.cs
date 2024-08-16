namespace ApiTransactionHistory.Domain;

public class TransactionHistoryToCoupons
{
    public int Id { get; set; }
    public int TransactionHistoryId { get; set; }
    public TransactionHistory TransactionHistory { get; set; }
    public ICollection<int> CouponsId { get; set; }
}
