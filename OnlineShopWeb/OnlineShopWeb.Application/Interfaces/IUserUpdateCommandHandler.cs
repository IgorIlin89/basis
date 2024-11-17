using OnlineShopWeb.Application.Commands.User;

namespace OnlineShopWeb.Application.Interfaces;

public interface IUserUpdateCommandHandler
{
    Task<Domain.User> Handle(UserUpdateCommand command);
}