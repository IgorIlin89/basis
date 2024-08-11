using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IGetProductListCommandHandler
{
    List<Product> Handle();
}