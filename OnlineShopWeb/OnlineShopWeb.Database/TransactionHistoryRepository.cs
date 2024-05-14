using Microsoft.EntityFrameworkCore;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Domain;
//using System.Data.Entity;

namespace OnlineShopWeb.Database;

internal class TransactionHistoryRepository : ITransactionHistoryRepository
{
    private OnlineShopWebDbContext _dbContext;

    public TransactionHistoryRepository(OnlineShopWebDbContext onlineShopWebDbContext)
    {
        _dbContext = onlineShopWebDbContext;
    }

    public void DeleteTransactionFromHistory(int transactionHistoryId)
    {
        var transactionHistory = GetTransactionHistoryItemById(transactionHistoryId);
        _dbContext.TransactionHistory.Remove(transactionHistory);
        _dbContext.SaveChanges();
    }

    public List<TransactionHistory> GetTransactionHistoryList(int userId)
    {
        return _dbContext.TransactionHistory.Include("Coupons").Where(o => o.UserId == userId).ToList();
    }

    public TransactionHistory? GetTransactionHistoryItemById(int id)
    {
        return _dbContext.TransactionHistory.FirstOrDefault(o => o.Id == id);
    }
}
