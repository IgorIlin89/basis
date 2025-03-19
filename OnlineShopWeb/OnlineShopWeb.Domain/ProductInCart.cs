using OnlineShopWeb.Domain.Exceptions;

namespace OnlineShopWeb.Domain;

public class ProductInCart
{
    public int ProductId { get; private init; }
    public int Count { get; private init; }
    public decimal PricePerProduct { get; private init; }

    private ProductInCart()
    {

    }

    public static ProductInCart Create(
        int productId,
        int count,
        decimal pricePerProduct)
    {
        if (productId <= 0 || count <= 0 || pricePerProduct <= 0)
        {
            throw new DomainException("A ProductInCart object needs a ProductId, PricePerProduct and Count bigger than 0"); ;
        }

        return new ProductInCart
        {
            ProductId = productId,
            Count = count,
            PricePerProduct = pricePerProduct
        };
    }
}
