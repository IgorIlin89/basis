namespace ApiTransactionHistory.Domain.Dtos;

public class TransactionHistoryToCouponsDto
{
    public int TransactionHistoriesId { get; set; }
    public ICollection<int> CouponsId { get; set; }
}
