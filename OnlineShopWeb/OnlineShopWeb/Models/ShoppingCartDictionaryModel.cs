using Microsoft.AspNetCore.Html;

namespace OnlineShopWeb.Models;

public class ShoppingCartDictionaryModel
{
    public Dictionary<int, ShoppingCartModel> ShoppingCartModelDictionary { get; set; } = new Dictionary<int, ShoppingCartModel>();
    public Dictionary<int, CouponModel> CouponModelDictionary { get; set; } = new Dictionary<int, CouponModel>();
}
