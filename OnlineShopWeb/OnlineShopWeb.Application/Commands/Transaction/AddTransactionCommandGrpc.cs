using GrpcAdapter.DTOs;

namespace OnlineShopWeb.Application.Commands.Transaction;

public record AddTransactionCommandGrpc(string UserId,
    List<ProductInCartDto> ProductInCartDtos,
    List<TransactionToCouponsDto>? TransactionToCouponsDto)
{

}