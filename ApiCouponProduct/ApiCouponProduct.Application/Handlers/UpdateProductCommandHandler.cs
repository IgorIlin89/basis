using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Application.Handlers;

public class UpdateProductCommandHandler(IUnitOfWork UnitOfWork, IProductRepository Repository) : IUpdateProductCommandHandler
{
    public Product Handle(UpdateProductCommand command)
    {
        var product = Repository.Update(command.ProductToUpdate);
        UnitOfWork.SaveChanges();
        return product;
    }
}
