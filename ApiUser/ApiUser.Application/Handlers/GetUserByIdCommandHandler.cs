using ApiUser.Application.Commands;
using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Database.Interfaces;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers;

public class GetUserByIdCommandHandler(IUserRepository Repository) : IGetUserByIdCommandHandler
{
    public User Handle(GetUserByIdCommand command)
    {
        return Repository.GetUserById(Int32.Parse(command.Id));
    }
}
