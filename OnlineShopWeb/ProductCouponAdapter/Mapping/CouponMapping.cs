using OnlineShopWeb.Domain;
using ProductCouponAdapter.DTOs;

namespace ProductCouponAdapter.Mapping;

public static class CouponMapping
{
    public static Coupon MapToDomain(this CouponDto couponDto) =>
        new Coupon
        {
            Code = couponDto.Code,
            AmountOfDiscount = couponDto.AmountOfDiscount,
            TypeOfDiscount = (OnlineShopWeb.Domain.TypeOfDiscountCoupon)couponDto.TypeOfDiscount,
            MaxNumberOfUses = couponDto.MaxNumberOfUses,
            StartDate = couponDto.StartDate,
            EndDate = couponDto.EndDate
        };

    public static CouponDto MapToDto(this Coupon coupon) =>
        new CouponDto
        {
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount.MapToDomain(),
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate
        };

    public static OnlineShopWeb.Domain.TypeOfDiscountCoupon MapToDomain(this DTOs.TypeOfDiscountCoupon typeOfDiscountDto) =>
        typeOfDiscountDto switch
        {
            DTOs.TypeOfDiscountCoupon.Percentage => OnlineShopWeb.Domain.TypeOfDiscountCoupon.Percentage,
            DTOs.TypeOfDiscountCoupon.Total => OnlineShopWeb.Domain.TypeOfDiscountCoupon.Total,
            _ => throw new NotImplementedException()
        };

    public static DTOs.TypeOfDiscountCoupon MapToDomain(this OnlineShopWeb.Domain.TypeOfDiscountCoupon typeOfDiscount) =>
    typeOfDiscount switch
    {
        OnlineShopWeb.Domain.TypeOfDiscountCoupon.Percentage => DTOs.TypeOfDiscountCoupon.Percentage,
        OnlineShopWeb.Domain.TypeOfDiscountCoupon.Total => DTOs.TypeOfDiscountCoupon.Total,
        _ => throw new NotImplementedException()
    };

    public static List<Coupon> MapToCouponList(this List<CouponDto> couponDtoList) =>
        couponDtoList.Select(o => o.MapToDomain()).ToList();
}