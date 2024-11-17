using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using UserAdapter;

namespace OnlineShopWeb.Application.Handlers.User;

public class UserAddCommandHandler(IUserAdapter userAdapter) : IUserAddCommandHandler
{
    public async Task<Domain.User> Handle(UserAddCommand command)
    {
        var userToAdd = new Domain.User
        {
            EMail = command.EMail,
            GivenName = command.GivenName,
            Surname = command.Surname,
            Age = command.Age,
            Country = command.Country,
            City = command.City,
            Street = command.City,
            HouseNumber = command.HouseNumber,
            PostalCode = command.PostalCode,
            Password = command.Password

        };

        var result = await userAdapter.UserAdd(userToAdd);
        return result;
    }
}
