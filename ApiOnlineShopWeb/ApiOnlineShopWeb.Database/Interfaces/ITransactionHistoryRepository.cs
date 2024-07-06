using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Database.Interfaces;

public interface ITransactionHistoryRepository
{
    void BuyShoppingCartItems(TransactionHistory transactionHistory);
    List<TransactionHistory> GetTransactionHistoryList(int userId);
}