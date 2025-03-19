namespace OnlineShopWeb.TransferObjects.Models.ListModels;

public class ShoppingCartListModel
{
    public List<ProductInCartModel> ShoppingCartModelList { get; init; } = new List<ProductInCartModel>();
    public List<TransactionCouponModel> CouponModelList { get; init; } = new List<TransactionCouponModel>();
}
