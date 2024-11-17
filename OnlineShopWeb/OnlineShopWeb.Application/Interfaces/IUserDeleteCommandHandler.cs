using OnlineShopWeb.Application.Commands.User;

namespace OnlineShopWeb.Application.Interfaces;

public interface IUserDeleteCommandHandler
{
    void Handle(UserDeleteCommand command);
}