using ApiCouponProduct.Domain;
using ApiCouponProduct.Domain.Dtos;
using ApiCouponProduct.Domain.Exceptions;

namespace ApiCouponProduct.Application.Commands;

public record AddCouponCommand
{
    public Coupon CouponToAdd { get; init; }

    public AddCouponCommand(AddCouponDto couponDto)
    {
        if (couponDto is null)
        {
            throw new NotFoundException($"Can not add null as a new coupon");
        }

        CouponToAdd = couponDto.MapToCoupon();
    }
}
