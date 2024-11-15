using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetUserByEmailCommandHandler
{
    User Handle(GetUserByEmailCommand command);
}