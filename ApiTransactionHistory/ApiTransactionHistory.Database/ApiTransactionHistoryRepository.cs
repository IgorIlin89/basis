using ApiTransactionHistory.Domain;

namespace ApiTransactionHistory.Database;

internal class ApiTransactionHistoryRepository
{
    private readonly ApiTransactionHistoryContext _context;
    public ApiTransactionHistoryRepository(ApiTransactionHistoryContext context)
    {
        _context = context;
    }
    public List<TransactionHistory> GetList(int id)
    {
        return _context.TransactionHistory.Where(o => o.Id == id).ToList();
    }

    public void Add(TransactionHistory transactionHistory,
        ProductInCart productInCart, TransactionHistoryToCoupons transactionHistoryToCoupons)
    {
        var result = _context.TransactionHistory.Add(transactionHistory);
    }
}
