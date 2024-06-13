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
    public User User { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
    public ICollection<Coupon>? Coupons { get; set; }
    public ICollection<ProductInCart> ProductsInCart { get; set; }
}
