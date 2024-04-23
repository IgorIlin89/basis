using Microsoft.AspNetCore.Html;

namespace OnlineShopWeb.Models;

public class ShoppingCartListModel
{
    public List<ProductInCart> ShoppingCartModelList { get; set; } = new List<ProductInCart>();
    public List<CouponModel> CouponModelList { get; set; } = new List<CouponModel>();
}
