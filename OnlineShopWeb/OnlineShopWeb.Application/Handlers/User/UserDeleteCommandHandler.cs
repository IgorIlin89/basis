using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;

namespace OnlineShopWeb.Application.Handlers.User;

public class UserDeleteCommandHandler(IUserAdapter userAdapter) : IUserDeleteCommandHandler
{
    public void Handle(UserDeleteCommand command)
    {
        userAdapter.UserDelete(command.UserId);
    }
}
