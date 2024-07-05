using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Database.Interfaces;

public interface ITransactionHistoryRepository
{
    void BuyShoppingCartItems(TransactionHistory transactionHistory);
    void DeleteTransactionFromHistory(int transactionHistoryId);
    TransactionHistory? GetTransactionHistoryItemById(int id);
    List<TransactionHistory> GetTransactionHistoryList(int userId);
}