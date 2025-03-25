namespace OnlineShopWeb.Models;

public class TransactionModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string ProductNames { get; set; }
    public decimal FinalPrice { get; set; }
    public int NumberOfCoupons { get; set; }
    public int Count { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
}