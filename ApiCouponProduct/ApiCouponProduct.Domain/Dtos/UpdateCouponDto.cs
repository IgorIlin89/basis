namespace ApiCouponProduct.Domain.Dtos;

public class UpdateCouponDto
{
    public int CouponId { get; set; }
    public string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscount TypeOfDiscount { get; set; }
    public long? MaxNumberOfUses { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}
