using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers;

public class UpdateCouponCommandHandler(IUnitOfWork UnitOfWork, ICouponRepository Repository) : IUpdateCouponCommandHandler
{
    public Coupon Handle(UpdateCouponCommand command)
    {
        var coupon = Repository.Update(command.CouponToUpdate);
        UnitOfWork.SaveChanges();
        return coupon;
    }
}
