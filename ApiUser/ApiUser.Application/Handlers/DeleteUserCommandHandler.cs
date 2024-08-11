using ApiUser.Application.Commands;
using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Database.Interfaces;

namespace ApiUser.Application.Handlers;

public class DeleteUserCommandHandler(IUnitOfWork UnitOfWork, IUserRepository Repository) : IDeleteUserCommandHandler
{
    public void Handle(DeleteUserCommand command)
    {
        Repository.Delete(Int32.Parse(command.Id));
        UnitOfWork.SaveChanges();
    }
}
