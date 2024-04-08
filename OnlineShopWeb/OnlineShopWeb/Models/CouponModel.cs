using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class CouponModel
{
    public int CouponId { get; set; }
    public string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscount TypeOfDiscount { get; set; }
    public long? MaxNumberOfUses { get; set; }
}
