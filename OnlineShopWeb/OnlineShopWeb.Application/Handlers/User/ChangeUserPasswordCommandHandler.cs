using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain.Commands;
using OnlineShopWeb.Domain.Interfaces;

namespace OnlineShopWeb.Application.Handlers.User;

public class ChangeUserPasswordCommandHandler(IUserAdapter userAdapter) :
    IChangeUserPasswordCommandHandler
{
    public async Task<Domain.User> Handle(ChangeUserPasswordCommand command)
    {
        //TODO make it without DTO into this handler, just user userId and Password
        var changePasswordDto = new ChangePasswordCommand
        {
            UserId = command.UserId,
            Password = command.Password
        };

        var result = await userAdapter.ChangeUserPassword(changePasswordDto);
        return result;
    }
}
