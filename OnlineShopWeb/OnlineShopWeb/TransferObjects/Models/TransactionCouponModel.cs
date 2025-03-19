namespace OnlineShopWeb.TransferObjects.Models;

public class TransactionCouponModel
{
    public string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscountTransactionCouponModel TypeOfDiscount { get; set; }
}
