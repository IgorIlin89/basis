using OnlineShopWeb.Application.Commands.Coupon;

namespace OnlineShopWeb.Application.Interfaces;

public interface ICouponDeleteCommandHandler
{
    void Handle(CouponDeleteCommand command);
}