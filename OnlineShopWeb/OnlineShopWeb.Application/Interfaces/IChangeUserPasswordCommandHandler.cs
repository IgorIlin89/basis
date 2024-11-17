using OnlineShopWeb.Application.Commands.User;

namespace OnlineShopWeb.Application.Interfaces;

public interface IChangeUserPasswordCommandHandler
{
    Task<Domain.User> Handle(ChangeUserPasswordCommand command);
}