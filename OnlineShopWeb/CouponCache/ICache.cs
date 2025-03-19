using OnlineShopWeb.Domain;

namespace CouponCache;

public interface ICache
{
    ValueTask<List<Coupon>> GetCoupons();
    Task Refresh(CancellationToken cancellationToken);
}