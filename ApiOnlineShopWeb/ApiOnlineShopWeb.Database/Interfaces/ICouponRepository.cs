using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Database.Interfaces;

public interface ICouponRepository
{
    void AddCoupon(Coupon coupon);
    void DeleteCoupon(int id);
    void EditCoupon(Coupon coupon);
    Coupon? GetCouponByCode(string? code);
    Coupon? GetCouponById(int? id);
    List<Coupon> GetCouponList();
}