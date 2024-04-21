using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database.Interfaces;

public interface IShoppingCartRepository
{
    void BuyShoppingCartItem(TransactionHistory transactionHistory);
    List<ShoppingCart> GetProductsInCartList(int userId);
    ShoppingCart? GetShoppingCartItemById(int id);
}