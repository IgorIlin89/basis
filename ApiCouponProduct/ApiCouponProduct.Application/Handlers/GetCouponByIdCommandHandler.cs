using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;
namespace ApiCouponProduct.Application.Handlers;

public class GetCouponByIdCommandHandler(IUnitOfWork UnitOfWork, ICouponRepository Repository) : IGetCouponByIdCommandHandler
{
    public Coupon Handle(GetCouponByIdCommand command)
    {
        var coupon = Repository.GetCouponById(command.Id);
        return coupon;
    }
}
