using ApiUser.Application.Commands;
using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Database.Interfaces;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers;

public class AddUserCommandHandler(IUnitOfWork UnitOfWork, IUserRepository UserRepository) : IAddUserCommandHandler
{
    public User Handle(AddUserCommand command)
    {
        var user = UserRepository.AddUser(command.UserToAdd);
        UnitOfWork.SaveChanges();
        return user;
    }
}
