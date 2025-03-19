using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using ProductCouponAdapter;
using ProductCouponAdapter.Mapping;

namespace OnlineShopWeb.Application.Handlers.Coupon;

public class CouponAddCommandHandler(IProductCouponAdapter productCouponAdapter) : ICouponAddCommandHandler
{
    public async Task<Domain.Coupon> Handle(CouponAddCommand command)
    {
        var couponToAdd = new Domain.Coupon
        {
            Code = command.Code,
            AmountOfDiscount = command.AmountOfDiscount,
            TypeOfDiscount = command.TypeOfDiscountCoupon.MapToDomain(),
            MaxNumberOfUses = command.MaxNumberOfUses,
            StartDate = command.StartDate,
            EndDate = command.EndDate
        };

        var result = await productCouponAdapter.CouponAdd(couponToAdd);
        return result;
    }
}
