using OnlineShopWeb.Application.Commands.User;

namespace OnlineShopWeb.Application.Interfaces;

public interface IGetUserListCommandHandler
{
    Task<List<Domain.User>> Handle(GetUserListCommand command);
}