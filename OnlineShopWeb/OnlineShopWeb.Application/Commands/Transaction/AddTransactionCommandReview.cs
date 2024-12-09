using TransactionAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Transaction;

public record AddTransactionCommandReview(string UserId,
    List<ProductInCartDto> ProductInCartDtos,
    List<TransactionToCouponsDto>? TransactionToCouponsDto)
{

}