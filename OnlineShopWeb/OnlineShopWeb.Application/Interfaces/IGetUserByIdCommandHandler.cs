using OnlineShopWeb.Application.Commands.User;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetUserByIdCommandHandler
{
    Task<Domain.User> Handle(GetUserByIdCommand command);
}