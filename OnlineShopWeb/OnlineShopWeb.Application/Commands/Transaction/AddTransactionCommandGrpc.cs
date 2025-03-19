using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Application.Commands.Transaction;

public record AddTransactionCommandGrpc(string UserId,
    IReadOnlyCollection<ProductInCart> ProductsInCart,
    IReadOnlyCollection<OnlineShopWeb.Domain.TransactionCoupon> TransactionCoupons)
{

}