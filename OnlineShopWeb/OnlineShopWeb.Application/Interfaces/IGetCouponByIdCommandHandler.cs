using OnlineShopWeb.Application.Commands.Coupon;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetCouponByIdCommandHandler
{
    Task<Domain.Coupon> Handle(GetCouponByIdCommand command);
}