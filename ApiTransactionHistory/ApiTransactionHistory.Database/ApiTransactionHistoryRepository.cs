using ApiTransactionHistory.Database.Interfaces;
using ApiTransactionHistory.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiTransactionHistory.Database;

internal class ApiTransactionHistoryRepository : IApiTransactionHistoryRepository
{
    private readonly ApiTransactionHistoryContext _context;
    public ApiTransactionHistoryRepository(ApiTransactionHistoryContext context)
    {
        _context = context;
    }
    public List<TransactionHistory> GetList(int id)
    {
        return _context.TransactionHistory
            .Include(o => o.ProductsInCart)
            .Include(o => o.Coupons)
            .Where(o => o.UserId == id).ToList();
    }

    public TransactionHistory Add(TransactionHistory transactionHistory)
    {
        var result = _context.TransactionHistory.Add(transactionHistory);
        return result.Entity;
    }
}
