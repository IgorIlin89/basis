using OnlineShopWeb.Application.Commands.Coupon;

namespace OnlineShopWeb.Application.Interfaces;

public interface ICouponUpdateCommandHandler
{
    Task<Domain.Coupon> Handle(CouponUpdateCommand command);
}