using OnlineShopWeb.Domain;
using ProductCouponAdapter.DTOs;

namespace ProductCouponAdapter.Mapping;

public static class CouponMapping
{
    public static Coupon MapToDto(this CouponDto couponDto) =>
        new Coupon
        {
            Id = couponDto.CouponId.Value,
            Code = couponDto.Code,
            AmountOfDiscount = couponDto.AmountOfDiscount,
            TypeOfDiscount = (TypeOfDiscount)couponDto.TypeOfDiscount,
            MaxNumberOfUses = couponDto.MaxNumberOfUses,
            StartDate = couponDto.StartDate,
            EndDate = couponDto.EndDate
        };

    public static CouponDto MapToCoupon(this Coupon coupon) =>
        new CouponDto
        {
            CouponId = coupon.Id,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount.MapToDomain(),
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate
        };

    public static TypeOfDiscount MapToDomain(this TypeOfDiscountDto typeOfDiscountDto) =>
        typeOfDiscountDto switch
        {
            TypeOfDiscountDto.Percentage => TypeOfDiscount.Percentage,
            TypeOfDiscountDto.Total => TypeOfDiscount.Total,
            _ => throw new NotImplementedException()
        };

    public static TypeOfDiscountDto MapToDomain(this TypeOfDiscount typeOfDiscount) =>
    typeOfDiscount switch
    {
        TypeOfDiscount.Percentage => TypeOfDiscountDto.Percentage,
        TypeOfDiscount.Total => TypeOfDiscountDto.Total,
        _ => throw new NotImplementedException()
    };

    public static List<Coupon> MapToCouponList(this List<CouponDto> couponDtoList) =>
        couponDtoList.Select(o => o.MapToDto()).ToList();
}