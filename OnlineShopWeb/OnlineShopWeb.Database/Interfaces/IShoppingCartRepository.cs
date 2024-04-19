using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database.Interfaces;

public interface IShoppingCartRepository
{
    void AddProductToCart(int productId, int userId, int couponId);
    void BuyShoppingCartItem(int shoppingCartId, int? couponId);
    void DeleteProductFromCart(int shoppingCartId);
    List<ShoppingCart> GetProductsInCartList(int userId);
}