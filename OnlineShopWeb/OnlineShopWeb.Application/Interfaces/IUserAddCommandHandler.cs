using OnlineShopWeb.Application.Commands.User;

namespace OnlineShopWeb.Application.Interfaces;

public interface IUserAddCommandHandler
{
    Domain.User Handle(UserAddCommand command);
}