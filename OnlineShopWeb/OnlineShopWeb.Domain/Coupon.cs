using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

public class Coupon
{
    public int Id;
    public string Code;
    public double AmountOfDiscount;
    public TypeOfDiscount TypeOfDiscount;
    public long? MaxNumberOfUses;
}
