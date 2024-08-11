using ApiCouponProduct.Application.Commands;

namespace ApiCouponProduct.Application.Handlers.Interfaces;

public interface IDeleteProductByIdCommandHandler
{
    void Handle(DeleteProductCommand command);
}