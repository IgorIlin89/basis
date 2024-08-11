using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IUpdateCouponCommandHandler
{
    Coupon Handle(UpdateCouponCommand command);
}