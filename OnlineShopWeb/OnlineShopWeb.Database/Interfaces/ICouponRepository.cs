using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database.Interfaces;

public interface ICouponRepository
{
    List<Coupon> GetCouponList();

    Coupon? GetCouponById(int? id);
    Coupon? GetCouponByCode(string? code);

    void AddCoupon(Coupon coupon);

    void DeleteCoupon(int id);
    public void EditCoupon(Coupon coupon);
}