using ApiTransactionHistory.Domain;
using ApiTransactionHistory.Domain.Dtos;
namespace ApiTransactionHistory.Application.Commands;

public record AddTransactionHistoryCommand
{
    public readonly TransactionHistory TransactionHistoryToAdd;
    public readonly List<ProductInCart> ProductsInCartToAdd;
    public readonly TransactionHistoryToCoupons TransactionHistoryToCouponToAdd;

    public AddTransactionHistoryCommand(AddTransactionHistoryDto addTransactionHistoryDto)
    {

        ProductsInCartToAdd = addTransactionHistoryDto.ProductsInCart;
        TransactionHistoryToCouponToAdd = addTransactionHistoryDto.Coupons;

        TransactionHistoryToAdd = TransactionHistoryToAdd{
            tr
        };

    }
}
