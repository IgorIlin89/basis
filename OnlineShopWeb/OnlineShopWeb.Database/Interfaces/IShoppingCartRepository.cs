using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database.Interfaces;

public interface IShoppingCartRepository
{
    void BuyShoppingCartItems(List<TransactionHistory> transactionHistoryList);
    List<ShoppingCart> GetProductsInCartList(int userId);
    ShoppingCart? GetShoppingCartItemById(int id);
}