using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

public class Coupon
{
    public int CouponId;
    public string Code;
    public double AmountOfDiscount;
    public ETypeOfDiscount TypeOfDiscount;
    public long? MaxNumberOfUses;
}
