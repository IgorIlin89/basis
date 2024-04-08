
namespace OnlineShopWeb.Domain;

public interface ICouponService
{
    void AddCoupon(int couponId, string code, double amountOfDiscount, TypeOfDiscount typeOfDiscount, long? maxNumberOfUses);
    bool Delete(int couponId);
    Coupon? GetCoupon(int couponId);
    List<Coupon> GetCouponList();
}