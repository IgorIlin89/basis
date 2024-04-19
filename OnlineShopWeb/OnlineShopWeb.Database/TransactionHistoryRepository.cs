using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Domain;

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
        return _dbContext.TransactionHistory.Where(o => o.UserId == userId).ToList();
    }

    public TransactionHistory? GetTransactionHistoryItemById(int id)
    {
        return _dbContext.TransactionHistory.FirstOrDefault(o => o.Id == id);
    }
}
