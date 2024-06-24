using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWebAPI.Domain;

public class Coupon
{
    public int Id { get; set; }
    public int CouponCode { get; set; }
    public int AmountOfDiscount { get; set; }
}
