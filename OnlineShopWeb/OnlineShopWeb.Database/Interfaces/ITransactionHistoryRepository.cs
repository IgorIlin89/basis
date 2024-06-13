using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database.Interfaces;

public interface ITransactionHistoryRepository
{
    void DeleteTransactionFromHistory(int transactionHistoryId);
    TransactionHistory? GetTransactionHistoryItemById(int id);
    List<TransactionHistory> GetTransactionHistoryList(int userId);
    public void BuyShoppingCartItems(TransactionHistory transactionHistory);
}