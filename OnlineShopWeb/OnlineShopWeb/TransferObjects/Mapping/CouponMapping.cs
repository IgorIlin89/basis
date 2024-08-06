using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Models;
using System.Security.Cryptography.X509Certificates;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class CouponMapping
{
    public static CouponModel MapToModel(this CouponDto couponDto)
    {
        var couponModel = new CouponModel();

        // if (couponDto.CouponId == null || !couponDto.CouponId.HasValue) is that the same?

        if (couponDto.MaxNumberOfUses is not null)
        {
            couponModel.MaxNumberOfUses = couponDto.MaxNumberOfUses;
        }

        if (couponDto.CouponId is not null)
        {
            couponModel.CouponId = couponDto.CouponId;
        }

        couponModel.Code = couponDto.Code;
        couponModel.AmountOfDiscount = couponDto.AmountOfDiscount;
        couponModel.TypeOfDiscount = couponDto.TypeOfDiscount;
        couponModel.StartDate = couponDto.StartDate;
        couponModel.EndDate = couponDto.EndDate;

        return couponModel;
    }
}
