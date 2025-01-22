using TransactionAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Transaction;

public record AddTransactionCommandHttp(string UserId,
    List<ProductInCartDto> ProductInCartDtos,
    List<TransactionToCouponsDto>? TransactionToCouponsDto)
{

}