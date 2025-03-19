namespace OnlineShopWeb.Domain;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset PaymentDate { get; set; }
    public decimal FinalPrice { get; set; }
    public required IReadOnlyCollection<ProductInCart> ProductsInCart { get; set; }
    public required IReadOnlyCollection<TransactionCoupon> Coupons { get; set; }

    private Transaction()
    {

    }

    public static Transaction Create(int id,
        int userId,
        DateTimeOffset paymentDate,
        decimal finalPrice,
        IReadOnlyCollection<ProductInCart> productsInCart,
        IReadOnlyCollection<TransactionCoupon> coupons
    )
    {
        var result = new Transaction
        {
            Id = id,
            UserId = userId,
            PaymentDate = paymentDate,
            FinalPrice = finalPrice,
            ProductsInCart = productsInCart,
            Coupons = coupons
        };

        return result;
    }
}
