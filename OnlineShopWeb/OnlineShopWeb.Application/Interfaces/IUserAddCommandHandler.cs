using OnlineShopWeb.Application.Commands.User;

namespace OnlineShopWeb.Application.Interfaces;

public interface IUserAddCommandHandler
{
    Task<Domain.User> Handle(UserAddCommand command);
}