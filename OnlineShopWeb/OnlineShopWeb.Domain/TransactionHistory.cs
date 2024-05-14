using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

public class TransactionHistory
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }

    public int Count { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public ICollection<Coupon>? Coupons { get; set; }
}
