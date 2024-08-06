using OnlineShopWeb.Domain;

namespace OnlineShopWeb.TransferObjects.Dtos;

public class TransactionHistoryDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public List<string>? CouponCodes { get; set; }
    public ICollection<ProductInCartDto> ProductsInCartDto { get; set; }
}
