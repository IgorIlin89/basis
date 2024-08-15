using ApiTransactionHistory.Database.Interfaces;

namespace ApiTransactionHistory.Database;

public class UnitOfWork(ApiTransactionHistoryContext DbContext) : IUnitOfWork
{
    public void SaveChanges()
    {
        DbContext.SaveChanges();
    }
}
