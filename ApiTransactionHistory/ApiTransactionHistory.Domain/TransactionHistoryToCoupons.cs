namespace ApiTransactionHistory.Domain;

public class TransactionHistoryToCoupons
{
    public ICollection<int> TransactionHistoriesId { get; set; }
    public ICollection<int> CouponsId { get; set; }
}
