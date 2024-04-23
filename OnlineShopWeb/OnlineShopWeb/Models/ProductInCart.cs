using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class ProductInCart
{
    public ProductModel ProductModelInCart { get; set; }
    public int count { get; set; }
}
