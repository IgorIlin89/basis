namespace OnlineShopWeb.Messages.V2;

public class TransactionCouponDto
{
    public required string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscountDto TypeOfDiscount { get; set; }
}
