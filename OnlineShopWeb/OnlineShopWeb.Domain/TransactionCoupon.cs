namespace OnlineShopWeb.Domain;

public class TransactionCoupon
{
    public required string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscountTransactionCoupon TypeOfDiscount { get; set; }
}
