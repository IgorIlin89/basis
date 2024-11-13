using OnlineShopWeb.Application.Commands;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain;
using UserAdapter;

namespace OnlineShopWeb.Application.Handlers;

public class GetUserByEmailCommandHandler(IUserAdapterProject UserAdapter) : IGetUserByEmailCommandHandler
{
    public User Handle(GetUserByEmailCommand command)
    {
        //TODO
        var user = new User
        {
            EMail = command.EMail
        };

        UserAdapter.GetUserByEmail(user);
        return new User();
    }
}
