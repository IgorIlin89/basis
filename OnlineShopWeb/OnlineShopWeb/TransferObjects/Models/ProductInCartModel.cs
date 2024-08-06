using OnlineShopWeb.Domain;

namespace OnlineShopWeb.TransferObjects.Models;

public class ProductInCartModel
{
    public ProductModel ProductModelInCart { get; set; }
    public int Count { get; set; }
}
