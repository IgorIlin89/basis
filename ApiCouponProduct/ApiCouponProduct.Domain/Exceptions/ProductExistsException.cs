namespace ApiCouponProduct.Domain.Exceptions;

public class ProductExistsException : Exception
{
    public ProductExistsException(string message)
        : base(message)
    {

    }

    public ProductExistsException(string message, Exception exception)
        : base(message, exception)
    {

    }
}
