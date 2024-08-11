using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers;

public class GetProductByIdCommandHandler(IProductRepository Repository) : IGetProductByIdCommandHandler
{
    public Product Handle(GetProductByIdCommand command)
    {
        return Repository.GetProductById(command.Id);
    }
}
