using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using UserAdapter;

namespace OnlineShopWeb.Application.Handlers.User;

public class UserAddCommandHandler(IUserAdapterProject userAdapter) : IUserAddCommandHandler
{
    public Domain.User Handle(UserAddCommand command)
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

        //TODO
        var result = userAdapter.UserAdd(userToAdd);

        return new Domain.User();
    }
}
