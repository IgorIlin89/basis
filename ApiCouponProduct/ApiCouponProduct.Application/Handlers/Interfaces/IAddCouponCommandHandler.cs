using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IAddCouponCommandHandler
{
    Coupon Handle(AddCouponCommand command);
}