using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Models;
using ProductCouponAdapter.DTOs;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class CouponMapping
{
    public static CouponModel MapToModel(this CouponDto couponDto) =>
        new CouponModel
        {
            CouponId = couponDto.CouponId,
            Code = couponDto.Code,
            AmountOfDiscount = couponDto.AmountOfDiscount,
            TypeOfDiscount = couponDto.TypeOfDiscount.MapToModel(),
            MaxNumberOfUses = couponDto.MaxNumberOfUses,
            StartDate = couponDto.StartDate,
            EndDate = couponDto.EndDate,
        };

    public static TypeOfDiscountModel MapToModel(this TypeOfDiscount typeOfDiscount) =>
        typeOfDiscount switch
        {
            TypeOfDiscount.Percentage => TypeOfDiscountModel.Percentage,
            TypeOfDiscount.Total => TypeOfDiscountModel.Total
        };

    public static TypeOfDiscountModel MapToModel(this TypeOfDiscountDto typeOfDiscount) =>
    typeOfDiscount switch
    {
        TypeOfDiscountDto.Percentage => TypeOfDiscountModel.Percentage,
        TypeOfDiscountDto.Total => TypeOfDiscountModel.Total
    };

    public static TypeOfDiscountDto MapToDto(this TypeOfDiscountModel typeOfDiscount) =>
    typeOfDiscount switch
    {
        TypeOfDiscountModel.Percentage => TypeOfDiscountDto.Percentage,
        TypeOfDiscountModel.Total => TypeOfDiscountDto.Total
    };

    public static CouponModel MapToModel(this Coupon coupon) =>
        new CouponModel
        {
            CouponId = coupon.Id,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount.MapToModel(),
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate,
        };
}
