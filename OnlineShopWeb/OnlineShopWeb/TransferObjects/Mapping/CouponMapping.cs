using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Models;
using System.Security.Cryptography.X509Certificates;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class CouponMapping
{
    public static CouponModel MapToModel(this CouponDto couponDto)
    {
        return new CouponModel
        {
            CouponId = couponDto.CouponId,
            Code = couponDto.Code,
            AmountOfDiscount = couponDto.AmountOfDiscount,
            TypeOfDiscount = couponDto.TypeOfDiscount,
            MaxNumberOfUses = couponDto.MaxNumberOfUses,
            StartDate = couponDto.StartDate,
            EndDate = couponDto.EndDate,
        };
    }
}
