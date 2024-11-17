using OnlineShopWeb.Domain;

namespace TransactionAdapter;

public interface ITransactionAdapter
{
    Task<Transaction> AddTransaction(AddTransaction transactionHistory);
    Task<List<Transaction>> GetTransactionList(string id);
}