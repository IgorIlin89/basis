using OnlineShopWeb.Application.Commands.User;
using UserAdapter;

namespace OnlineShopWeb.Application.Handlers.User;

public class GetUserListCommandHandler(IUserAdapterProject userAdapterProject)
{
    public List<Domain.User> Handle(GetUserListCommand command)
    {
        var result = userAdapterProject.GetUserList();
        //TODO
        return new List<Domain.User>();
    }
}
