using ApiUser.Application.Commands;
using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Database.Interfaces;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers;

public class ChangePasswordCommandHandler(IUnitOfWork UnitOfWork, IUserRepository Repository) : IChangePasswordCommandHandler
{
    public User Handle(ChangePasswordCommand command)
    {
        var user = Repository.ChangePassword(command.Id, command.Password);
        UnitOfWork.SaveChanges();
        return user;
    }
}
