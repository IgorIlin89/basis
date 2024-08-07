using ApiUser.Domain;

namespace ApiUser.Application.Handlers.Interfaces;

public interface IGetUserListCommandHandler
{
    List<User> Handle();
}