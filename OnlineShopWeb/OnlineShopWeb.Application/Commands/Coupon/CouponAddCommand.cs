using ProductCouponAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Coupon;

public record CouponAddCommand(string Code, double AmountOfDiscount,
    TypeOfDiscountCoupon TypeOfDiscountCoupon, long? MaxNumberOfUses, DateTimeOffset StartDate,
    DateTimeOffset EndDate)
{
}
