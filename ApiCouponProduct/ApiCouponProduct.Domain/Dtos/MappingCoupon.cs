namespace ApiCouponProduct.Domain.Dtos;

public static class MappingCoupon
{
    public static AddCouponDto MapToDto(this Coupon coupon)
    {
        return new AddCouponDto
        {
            CouponId = coupon.Id,
            Code = coupon.Code,
            AmountOfDiscount = coupon.AmountOfDiscount,
            TypeOfDiscount = coupon.TypeOfDiscount,
            MaxNumberOfUses = coupon.MaxNumberOfUses,
            StartDate = coupon.StartDate,
            EndDate = coupon.EndDate
        };
    }
    public static List<AddCouponDto> MapToDtoList(this List<Coupon> couponList)
    {
        var couponDtoList = new List<AddCouponDto>();

        foreach (var element in couponList)
        {
            couponDtoList.Add(element.MapToDto());
        }

        return couponDtoList;
    }
    public static Coupon MapToCoupon(this AddCouponDto couponDto)
    {
        var coupon = new Coupon();

        if (couponDto.CouponId is not null)
        {
            coupon.Id = couponDto.CouponId.Value;
        }

        coupon.Code = couponDto.Code;
        coupon.AmountOfDiscount = couponDto.AmountOfDiscount;
        coupon.TypeOfDiscount = couponDto.TypeOfDiscount;
        coupon.MaxNumberOfUses = couponDto.MaxNumberOfUses;
        coupon.StartDate = couponDto.StartDate;
        coupon.EndDate = couponDto.EndDate;

        return coupon;
    }
    public static Coupon MapToCoupon(this UpdateCouponDto updateCouponDto)
    {
        var coupon = new Coupon();

        coupon.Id = updateCouponDto.CouponId;
        coupon.Code = updateCouponDto.Code;
        coupon.AmountOfDiscount = updateCouponDto.AmountOfDiscount;
        coupon.TypeOfDiscount = updateCouponDto.TypeOfDiscount;
        coupon.MaxNumberOfUses = updateCouponDto.MaxNumberOfUses;
        coupon.StartDate = updateCouponDto.StartDate;
        coupon.EndDate = updateCouponDto.EndDate;

        return coupon;
    }
}
