using OnlineShopWeb.Application.Commands.Product;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetProductByIdCommandHandler
{
    Task<Domain.Product> Handle(GetProductByIdCommand command);
}