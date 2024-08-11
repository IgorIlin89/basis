using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers;

public class GetCouponListCommandHandler(ICouponRepository Repository) : IGetCouponListCommandHandler
{
    public List<Coupon> Handle()
    {
        return Repository.GetCouponList();
    }
}
