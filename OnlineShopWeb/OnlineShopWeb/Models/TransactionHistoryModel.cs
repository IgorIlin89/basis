using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class TransactionHistoryModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public string? CouponIds { get; set; }
    public string ProductName {  get; set; }
    public int Count { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
}