using OnlineShopWeb.Application.Commands.Coupon;

namespace OnlineShopWeb.Application.Interfaces;

public interface ICouponAddCommandHandler
{
    Task<Domain.Coupon> Handle(CouponAddCommand command);
}