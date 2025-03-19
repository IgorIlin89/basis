namespace TransactionAdapter.DTOs;

public class TransactionCouponDto
{
    public string Code { get; set; } = null!;
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscountTransactionCouponDto TypeOfDiscountDto { get; set; }
}
