using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Database.Interfaces;

public interface ICouponRepository
{
    Coupon AddCoupon(Coupon coupon);
    void Delete(int id);
    void Delete(string code);
    Coupon GetCouponByCode(string code);
    Coupon GetCouponById(int id);
    List<Coupon> GetCouponList();
    Coupon Update(Coupon coupon);
}