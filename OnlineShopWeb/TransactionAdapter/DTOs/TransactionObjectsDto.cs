namespace TransactionAdapter.DTOs;

public class TransactionObjectsDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserDto User { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
    public ICollection<CouponDto>? Coupons { get; set; }
    public ICollection<ProductInCartDto> ProductsInCart { get; set; }
}
