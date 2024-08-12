namespace ApiTransactionHistory.Domain.Dtos;

public class TransactionHistoryDto
{
    public int Id { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
    public int UserDtoId { get; set; }
    public UserInTransactionDto UserInTransaction { get; set; }
    public ICollection<CouponDtoInTransactionHistory>? Coupons { get; set; }
    //public ICollection<ProductInCart> ProductsInCart { get; set; }
}
