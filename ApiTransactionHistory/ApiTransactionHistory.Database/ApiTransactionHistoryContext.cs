using ApiTransactionHistory.Domain;
using Microsoft.EntityFrameworkCore;

namespace ApiTransactionHistory.Database;

public class ApiTransactionHistoryContext : DbContext
{
    public ApiTransactionHistoryContext(DbContextOptions<TransactionHistory> context)
        : base(context)
    {

    }

    public DbSet<TransactionHistory> TransactionHistory { get; set; }
}
