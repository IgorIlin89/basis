namespace OnlineShopWeb.Domain;

public class Coupon
{
    public string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscountCoupon TypeOfDiscount { get; set; }
    public long? MaxNumberOfUses { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}
