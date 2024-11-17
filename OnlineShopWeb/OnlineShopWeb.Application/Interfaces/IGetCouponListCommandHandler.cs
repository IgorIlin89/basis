using OnlineShopWeb.Application.Commands.Coupon;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetCouponListCommandHandler
{
    Task<List<Domain.Coupon>> Handle(GetCouponListCommand command);
}