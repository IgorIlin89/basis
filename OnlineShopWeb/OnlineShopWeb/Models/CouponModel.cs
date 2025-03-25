namespace OnlineShopWeb.Models;

public class CouponModel
{
    public int? CouponId { get; set; }
    public string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscountCouponModel TypeOfDiscount { get; set; }
    public long? MaxNumberOfUses { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}
