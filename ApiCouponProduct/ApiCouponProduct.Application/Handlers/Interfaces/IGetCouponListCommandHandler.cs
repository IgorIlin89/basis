using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IGetCouponListCommandHandler
{
    List<Coupon> Handle();
}