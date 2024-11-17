using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetUserByEmailCommandHandler
{
    Task<User> Handle(GetUserByEmailCommand command);
}