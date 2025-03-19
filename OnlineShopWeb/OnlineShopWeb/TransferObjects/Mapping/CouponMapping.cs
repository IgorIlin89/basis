using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class CouponMapping
{
    public static Coupon MapToDomain(this CouponModel model)
        => new Coupon
        {
            Code = model.Code,
            AmountOfDiscount = model.AmountOfDiscount,
            TypeOfDiscount = model.TypeOfDiscount.MapToDomain(),
            MaxNumberOfUses = model.MaxNumberOfUses,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
        };

    public static List<Coupon> MapToDomainList(this List<CouponModel> model)
        => model.Select(o => o.MapToDomain()).ToList();

    public static IReadOnlyCollection<CouponModel> MapToModelList(this IReadOnlyCollection<Coupon> couponList) =>
        couponList.Select(o => o.MapToModel()).ToList();

    public static TypeOfDiscountCouponModel MapToModel(this Domain.TypeOfDiscountCoupon typeOfDiscount) =>
        typeOfDiscount switch
        {
            Domain.TypeOfDiscountCoupon.Percentage => TypeOfDiscountCouponModel.Percentage,
            Domain.TypeOfDiscountCoupon.Total => TypeOfDiscountCouponModel.Total,
            _ => throw new NotImplementedException()
        };

    public static TypeOfDiscountCouponModel MapToModel(this ProductCouponAdapter.DTOs.TypeOfDiscountCoupon typeOfDiscount) =>
    typeOfDiscount switch
    {
        ProductCouponAdapter.DTOs.TypeOfDiscountCoupon.Percentage => TypeOfDiscountCouponModel.Percentage,
        ProductCouponAdapter.DTOs.TypeOfDiscountCoupon.Total => TypeOfDiscountCouponModel.Total,
        _ => throw new NotImplementedException()
    };

    public static ProductCouponAdapter.DTOs.TypeOfDiscountCoupon MapToDto(this TypeOfDiscountCouponModel typeOfDiscount) =>
    typeOfDiscount switch
    {
        TypeOfDiscountCouponModel.Percentage => ProductCouponAdapter.DTOs.TypeOfDiscountCoupon.Percentage,
        TypeOfDiscountCouponModel.Total => ProductCouponAdapter.DTOs.TypeOfDiscountCoupon.Total,
        _ => throw new NotImplementedException()
    };

    public static OnlineShopWeb.Domain.TypeOfDiscountCoupon MapToDomain(this TypeOfDiscountCouponModel typeOfDiscount) =>
    typeOfDiscount switch
    {
        TypeOfDiscountCouponModel.Percentage => OnlineShopWeb.Domain.TypeOfDiscountCoupon.Percentage,
        TypeOfDiscountCouponModel.Total => OnlineShopWeb.Domain.TypeOfDiscountCoupon.Total,
        _ => throw new NotImplementedException()
    };

    public static CouponModel MapToModel(this Coupon coupon) =>
        new CouponModel
        {
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount.MapToModel(),
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate,
        };
}
