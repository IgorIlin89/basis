using OnlineShopWeb.Domain;

namespace OnlineShopWeb.TransferObjects.Models.ListModels;

public class CouponListModel
{
    public List<CouponModel> CouponModelList { get; set; } = new List<CouponModel>();
}
