using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using UserAdapter;
using UserAdapter.DTOs;

namespace OnlineShopWeb.Application.Handlers.User;

public class ChangeUserPasswordCommandHandler(IUserAdapter userAdapter) : IChangeUserPasswordCommandHandler
{
    public async Task<Domain.User> Handle(ChangeUserPasswordCommand command)
    {
        //TODO make it without DTO into this handler, just user userId and Password
        var changePasswordDto = new ChangePasswordDto
        {
            UserId = command.UserId,
            Password = command.Password
        };

        var result = await userAdapter.ChangeUserPassword(changePasswordDto);
        return result;
    }
}
