namespace OnlineShopWeb.Domain;

public class AddTransactionToCoupons
{
    public int CouponId { get; set; }
    public string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscount TypeOfDiscountDto { get; set; }
}
