namespace ApiCouponProduct.Domain;

public enum ErrorStatusCode
{
    NotFound = 1,
    UserExists = 2,
    CouponExists = 3,
    ProductExists = 4,
    DefaultException = 999,
}
