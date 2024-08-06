using OnlineShopWeb.TransferObjects.Dtos;

namespace OnlineShopWeb.Adapters.Interfaces;

public interface ITransactionHistoryAdapter
{
    Task<TransactionHistoryDto> AddTransactionHistory(TransactionHistoryDto transactionHistoryDto);
    Task<List<TransactionHistoryObjectsDto>> GetTransactionHistoryList(string id);
}