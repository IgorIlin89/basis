using OnlineShopWeb.Application.Commands.Product;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetProductListCommandHandler
{
    Task<List<Domain.Product>> Handle(GetProductListCommand command);
}