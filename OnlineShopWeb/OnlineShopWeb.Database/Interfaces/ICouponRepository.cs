using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database.Interfaces;

public interface ICouponRepository
{
    List<Coupon> GetCouponList();

    Coupon? GetCoupon(int id);

    void AddCoupon(Coupon coupon);

    void DeleteCoupon(int id);
    public void EditCoupon(Coupon coupon);
}