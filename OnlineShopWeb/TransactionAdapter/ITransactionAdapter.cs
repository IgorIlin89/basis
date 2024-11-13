using TransactionAdapter.DTOs;

namespace TransactionAdapter;

public interface ITransactionAdapter
{
    Task<AddTransactionDto> AddTransaction(AddTransactionDto transactionHistoryDto);
    Task<List<TransactionObjectsDto>> GetTransactionHistoryList(string id);
}