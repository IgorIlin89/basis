using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IGetCouponByCodeCommandHandler
{
    Coupon Handle(GetCouponByCodeCommand command);
}