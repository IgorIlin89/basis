using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Interfaces;

namespace OnlineShopWeb.Application.Handlers.User;

public class UserUpdateCommandHandler(IUserAdapter userAdapter) : IUserUpdateCommandHandler
{
    public async Task<Domain.User> Handle(UserUpdateCommand command)
    {
        var userToUpdate = new Domain.User
        {
            Id = Int32.Parse(command.UserId),
            EMail = command.EMail,
            GivenName = command.GivenName,
            Surname = command.Surname,
            Age = command.Age,
            Country = command.Country,
            City = command.City,
            Street = command.Street,
            HouseNumber = command.HouseNumber,
            PostalCode = command.PostalCode
        };

        var result = await userAdapter.UserUpdate(userToUpdate);
        return result;
    }
}