using OnlineShopWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Database;

internal class CouponRepository : ICouponRepository
{
    private readonly OnlineShopWebDbContext _dbContext;
    public CouponRepository(OnlineShopWebDbContext onlineShopWebDbContext)
    {
        _dbContext = onlineShopWebDbContext;
    }
    public void AddCoupon(Coupon coupon)
    {
        _dbContext.Coupon.Add(coupon);
        _dbContext.SaveChanges();
    }

    public void DeleteCoupon(int id)
    {
        var entityEntry = GetCoupon(id);
        _dbContext.Remove(entityEntry);
        _dbContext.SaveChanges();
    }

    public void EditCoupon(Coupon coupon)
    {
        var entityEntry = GetCoupon(coupon.Id);

        entityEntry.Code = coupon.Code;
        entityEntry.AmountOfDiscount = coupon.AmountOfDiscount;
        entityEntry.TypeOfDiscount = coupon.TypeOfDiscount;
        entityEntry.MaxNumberOfUses = coupon.MaxNumberOfUses;

        _dbContext.SaveChanges();
    }

    public Coupon? GetCoupon(int id)
    {
        return _dbContext.Coupon.FirstOrDefault(o => o.Id == id);
    }

    public List<Coupon> GetCouponList()
    {
        return _dbContext.Coupon.ToList();
    }
}
