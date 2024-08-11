using ApiCouponProduct.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace ApiCouponProduct.Application.Commands;

public record GetCouponByCodeCommand
{
    public string Code { get; init; }
    public GetCouponByCodeCommand(string code)
    {
        if (code is null || code.IsNullOrEmpty())
        {
            throw new NotFoundException($"The code may not be null when searching for a coupon by code");
        }

        Code = code;
    }
}
