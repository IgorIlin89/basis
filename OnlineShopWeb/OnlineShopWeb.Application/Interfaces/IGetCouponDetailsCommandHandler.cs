using OnlineShopWeb.Application.Commands.Coupon;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetCouponDetailsCommandHandler
{
    Task<Domain.Coupon> Handle(GetCouponDetailsCommand command);
}