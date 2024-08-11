namespace ApiCouponProduct.Domain.Exceptions;

public class CouponExistsException : Exception
{
    public CouponExistsException(string message)
        : base(message)
    {

    }

    public CouponExistsException(string message, Exception exception)
        : base(message, exception)
    {

    }
}
