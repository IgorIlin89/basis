using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class CouponListModel
{
    public List<CouponModel> CouponModelList { get; set; } = new List<CouponModel>();
}
