using ApiUser.Application.Commands;
using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Database.Interfaces;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers;

public class UpdateUserCommandHandler(IUnitOfWork UnitOfWork, IUserRepository Repository) : IUpdateUserCommandHandler
{
    public User Handle(UpdateUserCommand command)
    {
        var user = Repository.Update(command.User);
        UnitOfWork.SaveChanges();
        return user;
    }
}
