using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IGetProductByIdCommandHandler
{
    Product Handle(GetProductByIdCommand command);
}