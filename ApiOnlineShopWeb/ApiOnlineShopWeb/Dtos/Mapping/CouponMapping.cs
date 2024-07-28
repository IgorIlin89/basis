using ApiOnlineShopWeb.Domain;
using static Azure.Core.HttpHeader;

namespace ApiOnlineShopWeb.Dtos.Mapping;

public static class CouponMapping
{
    public static CouponDto MapToDto(this Coupon coupon)
    {
        return new CouponDto
        {
            CouponId = coupon.Id,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount,
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate
        };
    }
}
