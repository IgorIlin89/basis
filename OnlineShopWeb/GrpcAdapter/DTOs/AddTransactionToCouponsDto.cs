namespace GrpcAdapter.DTOs;

public class AddTransactionToCouponsDto
{
    public int CouponId { get; set; }
    public string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscountDto TypeOfDiscountDto { get; set; }
}
