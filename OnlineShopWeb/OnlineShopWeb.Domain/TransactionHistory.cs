using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

public class TransactionHistory
{
    public int Id { get; set; }
    public int UserId {  get; set; }
    public int ProductId {  get; set; }
    public string? CouponIds {  get; set; }
    public DateTimeOffset PaymentDate { get; set; }
}
