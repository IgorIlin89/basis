using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database.Interfaces;

public interface ITransactionHistoryRepository
{
    List<TransactionHistory> GetTransactionHistoryList(int userId);
    public void BuyShoppingCartItems(TransactionHistory transactionHistory);
}