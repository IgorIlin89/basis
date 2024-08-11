using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IAddProductCommandHandler
{
    Product Handle(AddProductCommand command);
}