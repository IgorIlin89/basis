using ApiOnlineShopWeb.Database.Interfaces;
using ApiOnlineShopWeb.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiOnlineShopWeb.Database;
internal class TransactionHistoryRepository : ITransactionHistoryRepository
{
    private ApiOnlineShopWebContext _dbContext;

    public TransactionHistoryRepository(ApiOnlineShopWebContext onlineShopWebDbContext)
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
        var test = _dbContext.TransactionHistory
            .Include(o => o.Coupons)
            .Include(o => o.ProductsInCart)
            .ThenInclude(o => o.Product)
            .Include(o => o.User)
            .Where(o => o.UserId == userId)
            .AsNoTracking()
            .ToList();

        return test;
    }

    public void BuyShoppingCartItems(TransactionHistory transactionHistory)
    {
        _dbContext.TransactionHistory.Add(transactionHistory);
        _dbContext.SaveChanges();
    }

    public TransactionHistory? GetTransactionHistoryItemById(int id)
    {
        return _dbContext.TransactionHistory.FirstOrDefault(o => o.Id == id);
    }
}
