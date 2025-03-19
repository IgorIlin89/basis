namespace OnlineShopWeb.Domain;

public class AddTransaction
{
    public int UserId { get; set; }
    public required IReadOnlyCollection<ProductInCart> ProductsInCart { get; set; }
    public required IReadOnlyCollection<TransactionCoupon> Coupons { get; set; }

    private AddTransaction()
    {

    }

    public static AddTransaction Create(
        int userId,
        IReadOnlyCollection<ProductInCart> productsInCart,
        IReadOnlyCollection<TransactionCoupon> coupons
    )
    {
        var result = new AddTransaction
        {
            UserId = userId,
            ProductsInCart = productsInCart,
            Coupons = coupons
        };

        return result;
    }
}
