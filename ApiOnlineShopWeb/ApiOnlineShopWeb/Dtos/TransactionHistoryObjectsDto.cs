using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Dtos;

public class TransactionHistoryObjectsDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
    public ICollection<Coupon>? Coupons { get; set; }
    public ICollection<ProductInCart> ProductsInCart { get; set; }
}
