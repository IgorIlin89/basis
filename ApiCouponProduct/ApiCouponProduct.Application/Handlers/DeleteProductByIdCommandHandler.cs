using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;

namespace ApiCouponProduct.Application.Handlers;

public class DeleteProductByIdCommandHandler(IUnitOfWork UnitOfWork, IProductRepository Repository) : IDeleteProductByIdCommandHandler
{
    public void Handle(DeleteProductCommand command)
    {
        Repository.Delete(command.Id);
        UnitOfWork.SaveChanges();
    }
}
