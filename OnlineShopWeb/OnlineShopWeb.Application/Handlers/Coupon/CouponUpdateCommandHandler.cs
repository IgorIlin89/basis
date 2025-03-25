using OnlineShopWeb.Application.Commands.Coupon;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;
using ProductCouponAdapter.Mapping;

namespace OnlineShopWeb.Application.Handlers.Coupon;

public class CouponUpdateCommandHandler(IProductCouponAdapter productCouponAdapter) : ICouponUpdateCommandHandler
{
    public async Task<Domain.Coupon> Handle(CouponUpdateCommand command)
    {
        var couponToUpdate = new Domain.Coupon
        {
            Code = command.Code,
            AmountOfDiscount = command.AmountOfDiscount,
            TypeOfDiscount = command.TypeOfDiscount.MapToDomain(),
            MaxNumberOfUses = command.MaxNumberOfUses,
            StartDate = command.StartDate,
            EndDate = command.EndDate
        };

        var result = await productCouponAdapter.CouponUpdate(couponToUpdate);
        return result;
    }
}