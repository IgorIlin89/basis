using Microsoft.AspNetCore.Html;

namespace OnlineShopWeb.Models;

public class ShoppingCartListModel
{
    public List<ShoppingCartModel> ShoppingCartModelList { get; set; } = new List<ShoppingCartModel>();
    public string? CouponCode { get; set; }
    public string serializedList { get; set; }
}
