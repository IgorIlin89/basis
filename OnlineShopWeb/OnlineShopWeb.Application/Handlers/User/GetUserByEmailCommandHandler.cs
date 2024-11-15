using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using UserAdapter;

namespace OnlineShopWeb.Application.Handlers.User;

public class GetUserByEmailCommandHandler(IUserAdapterProject UserAdapter) : IGetUserByEmailCommandHandler
{
    public Domain.User Handle(GetUserByEmailCommand command)
    {
        //TODO
        var user = new Domain.User
        {
            EMail = command.EMail
        };

        //TODO
        var result = UserAdapter.GetUserByEmail(user);

        return new Domain.User();
    }
}
