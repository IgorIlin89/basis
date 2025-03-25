namespace OnlineShopWeb.Models;

public class ShoppingCartModel
{
    public List<ProductInCartModel> ShoppingCartModelList { get; init; } = new List<ProductInCartModel>();
    public List<TransactionCouponModel> CouponModelList { get; init; } = new List<TransactionCouponModel>();
}
