using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers;

public class GetCouponByCodeCommandHandler(IUnitOfWork UnitOfWork, ICouponRepository Repository) : IGetCouponByCodeCommandHandler
{
    public Coupon Handle(GetCouponByCodeCommand command)
    {
        var coupon = Repository.GetCouponByCode(command.Code);
        return coupon;
    }
}
