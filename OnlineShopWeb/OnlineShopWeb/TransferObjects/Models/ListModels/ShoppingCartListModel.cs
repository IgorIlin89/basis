using Microsoft.AspNetCore.Html;

namespace OnlineShopWeb.TransferObjects.Models.ListModels;

public class ShoppingCartListModel
{
    public List<ProductInCartModel> ShoppingCartModelList { get; set; } = new List<ProductInCartModel>();
    public List<CouponModel> CouponModelList { get; set; } = new List<CouponModel>();
}
