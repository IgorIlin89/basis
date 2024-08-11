using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;


namespace ApiCouponProduct.Application.Handlers;

public class AddCouponCommandHandler(IUnitOfWork UnitOfWork, ICouponRepository Repository) : IAddCouponCommandHandler
{
    public Coupon Handle(AddCouponCommand command)
    {
        var coupon = Repository.AddCoupon(command.CouponToAdd);
        UnitOfWork.SaveChanges();
        return coupon;
    }
}
