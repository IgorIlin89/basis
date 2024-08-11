using ApiCouponProduct.Database.Interfaces;

namespace ApiCouponProduct.Database;

public class UnitOfWork(ApiCouponProductContext DbContext) : IUnitOfWork
{
    public void SaveChanges()
    {
        DbContext.SaveChanges();
    }
}
