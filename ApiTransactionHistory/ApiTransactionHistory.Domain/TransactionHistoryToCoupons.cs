namespace ApiTransactionHistory.Domain;

public class TransactionHistoryToCoupons
{
    public int Id { get; set; }
    public int TransactionHistoryId { get; set; }
    public TransactionHistory TransactionHistory { get; set; }
    public List<int> CouponsId { get; set; }
}
