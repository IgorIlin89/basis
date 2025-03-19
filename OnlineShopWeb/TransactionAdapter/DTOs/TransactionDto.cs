namespace TransactionAdapter.DTOs;

public class TransactionDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
    public required IReadOnlyCollection<ProductInCartDto> ProductsInCartDto { get; set; }
    public required IReadOnlyCollection<TransactionCouponDto> CouponsDto { get; set; }
}
