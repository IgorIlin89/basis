using ApiCouponProduct.Domain;
using ApiCouponProduct.Domain.Dtos;

namespace ApiCouponProduct.Application.Commands;

public record UpdateCouponCommand
{
    public Coupon CouponToUpdate { get; init; }

    public UpdateCouponCommand(UpdateCouponDto updateCouponDto)
    {
        CouponToUpdate = updateCouponDto.MapToCoupon();
    }
}
