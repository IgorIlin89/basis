using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Database.Interfaces;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers;

public class GetUserListCommandHandler(IUnitOfWork UnitOfWork, IUserRepository Repository) : IGetUserListCommandHandler
{
    public List<User> Handle()
    {
        return Repository.GetUserList();
    }
}
