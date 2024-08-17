using ApiTransactionHistory.Domain;

namespace ApiTransactionHistory.Database.Interfaces;

public interface IApiTransactionHistoryRepository
{
    TransactionHistory Add(TransactionHistory transactionHistory);
    List<TransactionHistory> GetList(int id);
}