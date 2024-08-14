using ApiTransactionHistory.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiTransactionHistory.Database;

public class ApiTransactionHistoryContext : DbContext
{
    public ApiTransactionHistoryContext(DbContextOptions<ApiTransactionHistoryContext> context)
        : base(context)
    {

    }

    public DbSet<TransactionHistory> TransactionHistory { get; set; }
}
