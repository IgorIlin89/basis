namespace OnlineShopWeb.Domain.Commands;

public class AddTransactionCommand
{
    public int UserId { get; set; }
    public required IReadOnlyCollection<ProductInCart> ProductsInCart { get; set; }
    public required IReadOnlyCollection<TransactionCoupon> Coupons { get; set; }

    private AddTransactionCommand()
    {

    }

    public static AddTransactionCommand Create(
        int userId,
        IReadOnlyCollection<ProductInCart> productsInCart,
        IReadOnlyCollection<TransactionCoupon> coupons
    )
    {
        var result = new AddTransactionCommand
        {
            UserId = userId,
            ProductsInCart = productsInCart,
            Coupons = coupons
        };

        return result;
    }
}
