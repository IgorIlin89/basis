using ApiCouponProduct.Application.Commands;
using ApiCouponProduct.Application.Handlers.Interfaces;
using ApiCouponProduct.Database.Interfaces;


namespace ApiCouponProduct.Application.Handlers;

public class DeleteCouponCommandHandler(IUnitOfWork UnitOfWork, ICouponRepository Repository) : IDeleteCouponCommandHandler
{
    public void Handle(DeleteCouponCommand command)
    {
        Repository.Delete(command.Id);
        UnitOfWork.SaveChanges();
    }
}
