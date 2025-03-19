using ProductCouponAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Coupon;

public record CouponUpdateCommand(int CouponId, string Code, double AmountOfDiscount,
        TypeOfDiscountCoupon TypeOfDiscount, long? MaxNumberOfUses, DateTimeOffset StartDate,
        DateTimeOffset EndDate)
{
}
