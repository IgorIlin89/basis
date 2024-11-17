using OnlineShopWeb.Application.Commands.Coupon;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetCouponByCodeCommandHandler
{
    Task<Domain.Coupon> Handle(GetCouponByCodeCommand command);
}