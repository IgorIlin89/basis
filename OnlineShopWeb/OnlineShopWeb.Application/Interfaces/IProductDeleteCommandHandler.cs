using OnlineShopWeb.Application.Commands.Product;

namespace OnlineShopWeb.Application.Interfaces;

public interface IProductDeleteCommandHandler
{
    void Handle(ProductDeleteCommand command);
}