using OnlineShopWeb.Domain;
using TransactionAdapter.DTOs;

namespace TransactionAdapter;

public interface ITransactionAdapter
{
    Task<Transaction> AddTransaction(string userId, List<ProductInCartDto> productInCartDtoList,
        List<TransactionToCouponsDto>? couponDtoList);
    Task<List<Transaction>> GetTransactionList(string id);
}