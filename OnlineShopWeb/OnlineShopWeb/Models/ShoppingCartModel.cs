using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class ShoppingCartModel
{
    public ProductModel ProductInCart { get; set; }
    public int count { get; set; }
}
