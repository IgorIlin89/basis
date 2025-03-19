using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Application.Commands.Transaction;

public record AddTransactionCommandHttp
{
    public string UserId { get; private init; }
    public IReadOnlyCollection<ProductInCart> ProductInCarts { get; private init; }
    public IReadOnlyCollection<Domain.TransactionCoupon> Coupons { get; private init; }
    public AddTransactionCommandHttp(string userId,
    IReadOnlyCollection<ProductInCart> productsInCart,
    IReadOnlyCollection<Domain.TransactionCoupon> coupons)
    {
        UserId = userId;
        ProductInCarts = productsInCart;
        Coupons = coupons;
    }
}