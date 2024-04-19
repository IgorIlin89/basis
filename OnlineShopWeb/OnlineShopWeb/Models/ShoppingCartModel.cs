using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class ShoppingCartModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public int CouponId { get; set; }
    public string ProductName {  get; set; }
}
