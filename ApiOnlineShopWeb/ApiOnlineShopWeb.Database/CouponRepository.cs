using ApiOnlineShopWeb.Database.Interfaces;
using ApiOnlineShopWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnlineShopWeb.Database;

internal class CouponRepository : ICouponRepository
{
    private readonly ApiOnlineShopWebContext _dbContext;
    public CouponRepository(ApiOnlineShopWebContext onlineShopWebDbContext)
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
        var entityEntry = GetCouponById(id);
        _dbContext.Remove(entityEntry);
        _dbContext.SaveChanges();
    }

    public void EditCoupon(Coupon coupon)
    {
        var entityEntry = GetCouponById(coupon.Id);

        entityEntry.Code = coupon.Code;
        entityEntry.AmountOfDiscount = coupon.AmountOfDiscount;
        entityEntry.TypeOfDiscount = coupon.TypeOfDiscount;
        entityEntry.MaxNumberOfUses = coupon.MaxNumberOfUses;

        _dbContext.SaveChanges();
    }

    public Coupon? GetCouponById(int? id)
    {
        return _dbContext.Coupon.FirstOrDefault(o => o.Id == id);
    }

    public Coupon? GetCouponByCode(string? code)
    {
        return _dbContext.Coupon.FirstOrDefault(o => o.Code == code);
    }

    public List<Coupon> GetCouponList()
    {
        return _dbContext.Coupon.ToList();
    }
}
