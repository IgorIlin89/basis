using OnlineShopWeb.Application.Commands.Product;

namespace OnlineShopWeb.Application.Interfaces;

public interface IProductUpdateCommandHandler
{
    Task<Domain.Product> Handle(ProductUpdateCommand command);
}