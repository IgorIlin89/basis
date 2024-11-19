using OnlineShopWeb.Messages.V1;

namespace OnlineShopWeb.Application.Commands.Transaction;

public record AddTransactionCommandMessages
{
    public string UserId { get; init; }

    public List<AddProductInCartDto> ProductInCartDtos { get; init; }
    public List<AddTransactionToCouponsDto>? TransactionToCouponsDto { get; init; }

    public AddTransactionCommandMessages(string userId, List<AddProductInCartDto> productInCartDtos,
        List<AddTransactionToCouponsDto>? transactionToCouponsDto)
    {
        UserId = userId;
        ProductInCartDtos = productInCartDtos;
        TransactionToCouponsDto = transactionToCouponsDto;
    }
}
