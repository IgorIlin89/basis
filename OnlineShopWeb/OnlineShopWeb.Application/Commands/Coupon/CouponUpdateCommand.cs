using ProductCouponAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Coupon;

public record CouponUpdateCommand
{
    public int CouponId;
    public string Code;
    public double AmountOfDiscount;
    public TypeOfDiscountDto TypeOfDiscount;
    public long? MaxNumberOfUses;
    public DateTimeOffset StartDate;
    public DateTimeOffset EndDate;

    public CouponUpdateCommand(int couponId, string code, double amountOfDiscount,
        TypeOfDiscountDto typeOfDiscount, long? maxNumberOfUses, DateTimeOffset startDate,
        DateTimeOffset endDate)
    {
        CouponId = couponId;
        Code = code;
        AmountOfDiscount = amountOfDiscount;
        TypeOfDiscount = typeOfDiscount;
        MaxNumberOfUses = maxNumberOfUses;
        StartDate = startDate;
        EndDate = endDate;
    }
}
