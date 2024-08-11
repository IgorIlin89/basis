namespace ApiCouponProduct.Domain;

public class Coupon
{
    public int Id { get; set; }
    public string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscount TypeOfDiscount { get; set; }
    public long? MaxNumberOfUses { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}