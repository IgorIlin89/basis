using ApiCouponProduct.Database.Interfaces;

namespace ApiCouponProduct.Database;

internal class UnitOfWork(ApiCouponProductContext DbContext) : IUnitOfWork
{
    public void SaveChanges()
    {
        DbContext.SaveChanges();
    }
}
