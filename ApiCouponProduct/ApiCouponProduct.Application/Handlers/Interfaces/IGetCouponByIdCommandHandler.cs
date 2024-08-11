using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IGetCouponByIdCommandHandler
{
    Coupon Handle(GetCouponByIdCommand command);
}