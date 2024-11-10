using OnlineShopWeb.TransferObjects.Dtos;

namespace OnlineShopWeb.Adapters.Interfaces;

public interface ITransactionAdapter
{
    Task<AddTransactionDto> AddTransaction(AddTransactionDto transactionHistoryDto);
    Task<List<TransactionObjectsDto>> GetTransactionHistoryList(string id);
}