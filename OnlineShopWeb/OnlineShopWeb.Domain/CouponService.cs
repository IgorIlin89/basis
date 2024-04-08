using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

public class CouponService : ICouponService
{

    private List<Coupon> CouponList = new List<Coupon>
    {
        new Coupon { CouponId = 0, Code = "swD23", AmountOfDiscount = 15, TypeOfDiscount = TypeOfDiscount.Percentage, MaxNumberOfUses = 100 },
        new Coupon { CouponId = 1, Code = "dsf4wsdf", AmountOfDiscount = 150, TypeOfDiscount = TypeOfDiscount.Total, MaxNumberOfUses = 750 },
        new Coupon { CouponId = 2, Code = "sdfsdgfgh5fh", AmountOfDiscount = 25, TypeOfDiscount = TypeOfDiscount.Percentage, MaxNumberOfUses = 500 }
    };

    public List<Coupon> GetCouponList() { return CouponList; }

    public Coupon? GetCoupon(int couponId)
    {
        return CouponList.Where(o => o.CouponId == couponId).FirstOrDefault();
    }

    public bool Delete(int couponId)
    {
        var couponToDelete = CouponList.Where(o => o.CouponId == couponId).FirstOrDefault();
        return CouponList.Remove(couponToDelete);
    }

    public void AddCoupon(int couponId, string code, double amountOfDiscount, TypeOfDiscount typeOfDiscount, long? maxNumberOfUses)
    {
        CouponList.Add(new Coupon { CouponId = couponId, Code = code, AmountOfDiscount = amountOfDiscount, TypeOfDiscount = typeOfDiscount, MaxNumberOfUses = maxNumberOfUses });
    }

}