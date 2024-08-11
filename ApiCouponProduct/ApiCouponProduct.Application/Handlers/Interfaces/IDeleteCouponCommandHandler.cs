using ApiCouponProduct.Application.Commands;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IDeleteCouponCommandHandler
{
    void Handle(DeleteCouponCommand command);
}