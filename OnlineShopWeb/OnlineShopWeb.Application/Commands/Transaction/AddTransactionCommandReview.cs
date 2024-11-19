using TransactionAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Transaction;

public record AddTransactionCommandReview
{
    public string UserId { get; init; }
    public List<ProductInCartDto> ProductInCartDtos { get; init; }
    public List<TransactionToCouponsDto>? TransactionToCouponsDto { get; init; }

    public AddTransactionCommandReview(string userId, List<ProductInCartDto> productInCartDtos,
        List<TransactionToCouponsDto>? transactionToCouponsDto)
    {
        UserId = userId;
        ProductInCartDtos = productInCartDtos;
        TransactionToCouponsDto = transactionToCouponsDto;
    }

}
