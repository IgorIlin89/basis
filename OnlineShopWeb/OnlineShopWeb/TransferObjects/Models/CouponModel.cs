using OnlineShopWeb.Domain;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.TransferObjects.Models;

public class CouponModel
{
    public int? CouponId { get; set; }
    public string Code { get; set; }
    public double AmountOfDiscount { get; set; }
    public TypeOfDiscount TypeOfDiscount { get; set; }
    public long? MaxNumberOfUses { get; set; }
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
}
