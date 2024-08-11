using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IUpdateProductCommandHandler
{
    Product Handle(UpdateProductCommand command);
}