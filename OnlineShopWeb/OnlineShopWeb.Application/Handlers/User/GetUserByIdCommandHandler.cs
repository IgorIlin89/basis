using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using UserAdapter;

namespace OnlineShopWeb.Application.Handlers.User;

public class GetUserByIdCommandHandler(IUserAdapter userAdapter) : IGetUserByIdCommandHandler
{
    public async Task<Domain.User> Handle(GetUserByIdCommand command)
    {
        var received = await userAdapter.GetUserById(command.UserId);
        return received;
    }
}
