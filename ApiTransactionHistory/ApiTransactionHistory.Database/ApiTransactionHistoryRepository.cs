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

    //public TransactionHistory Add(TransactionHistory transactionHistory)
    //{
    //    //TODO
    //    return _context.TransactionHistory.Add(transactionHistory);
    //}
}
