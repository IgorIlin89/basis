using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;
using ApiCouponProduct.Domain;


namespace ApiCouponProduct.Application.Handlers;

public class AddProductCommandHandler(IUnitOfWork UnitOfWork, IProductRepository Repository) : IAddProductCommandHandler
{
    public Product Handle(AddProductCommand command)
    {
        var product = Repository.AddProduct(command.ProductToAdd);
        UnitOfWork.SaveChanges();
        return product;
    }
}
