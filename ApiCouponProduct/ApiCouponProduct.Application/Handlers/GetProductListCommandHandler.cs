using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers;

public class GetProductListCommandHandler(IProductRepository Repository) : IGetProductListCommandHandler
{
    public List<Product> Handle()
    {
        return Repository.GetProductList();
    }
}
