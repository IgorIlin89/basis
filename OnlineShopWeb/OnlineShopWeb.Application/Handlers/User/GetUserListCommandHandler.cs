using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using UserAdapter;

namespace OnlineShopWeb.Application.Handlers.User;

public class GetUserListCommandHandler(IUserAdapter userAdapterProject) : IGetUserListCommandHandler
{
    public async Task<List<Domain.User>> Handle(GetUserListCommand command)
    {
        var result = await userAdapterProject.GetUserList();
        return result;
    }
}
