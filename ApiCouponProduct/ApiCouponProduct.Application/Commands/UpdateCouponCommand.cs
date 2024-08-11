using ApiCouponProduct.Domain;
using ApiCouponProduct.Domain.Dtos;
using ApiCouponProduct.Domain.Exceptions;

namespace ApiCouponProduct.Application.Commands;

public record UpdateCouponCommand
{
    public Coupon CouponToUpdate { get; init; }

    public UpdateCouponCommand(CouponDto couponDto)
    {
        if (couponDto is null || couponDto.CouponId is null)
        {
            throw new NotFoundException($"Can not update coupon. Either you provided no id or the object is null");
        }

        CouponToUpdate = couponDto.MapToCoupon();
    }
}
