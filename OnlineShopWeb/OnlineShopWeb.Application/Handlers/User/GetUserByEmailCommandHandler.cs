using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using UserAdapter;

namespace OnlineShopWeb.Application.Handlers.User;

public class GetUserByEmailCommandHandler(IUserAdapter userAdapter) : IGetUserByEmailCommandHandler
{
    public async Task<Domain.User> Handle(GetUserByEmailCommand command)
    {
        var user = new Domain.User
        {
            EMail = command.EMail
        };

        var result = await userAdapter.GetUserByEmail(user);

        return result;
    }
}
