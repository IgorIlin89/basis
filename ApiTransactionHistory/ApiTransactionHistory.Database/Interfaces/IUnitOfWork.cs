namespace ApiTransactionHistory.Database.Interfaces;

public interface IUnitOfWork
{
    void SaveChanges();
}