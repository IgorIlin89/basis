using OnlineShopWeb.Application.Commands.Product;

namespace OnlineShopWeb.Application.Interfaces;

public interface IProductAddCommandHandler
{
    Task<Domain.Product> Handle(ProductAddCommand command);
}