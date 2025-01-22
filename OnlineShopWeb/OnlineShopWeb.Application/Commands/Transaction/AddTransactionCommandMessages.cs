using OnlineShopWeb.Messages.V1;

namespace OnlineShopWeb.Application.Commands.Transaction;

public record AddTransactionCommandMessages(string UserId,
    List<AddProductInCartDto> ProductInCartDtos,
    List<AddTransactionToCouponsDto>? TransactionToCouponsDto)
{

}
