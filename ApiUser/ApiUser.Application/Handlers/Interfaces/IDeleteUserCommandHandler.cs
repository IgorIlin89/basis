using ApiUser.Application.Commands;

namespace ApiUser.Application.Handlers.Interfaces;

public interface IDeleteUserCommandHandler
{
    void Handle(DeleteUserCommand command);
}