namespace ApiUser.Domain.Dtos;

public enum ErrorStatusCodeDto
{
    NotFound = 1,
    UserExists = 2,
    CouponExists = 3,
    ProductExists = 4,
    DefaultException = 999,
}
