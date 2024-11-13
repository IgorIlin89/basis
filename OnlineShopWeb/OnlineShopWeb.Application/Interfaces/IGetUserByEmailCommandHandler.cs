using OnlineShopWeb.Application.Commands;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetUserByEmailCommandHandler
{
    User Handle(GetUserByEmailCommand command);
}