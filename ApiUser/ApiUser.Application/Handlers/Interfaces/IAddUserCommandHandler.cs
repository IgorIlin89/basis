using ApiUser.Application.Commands;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers.Interfaces;

public interface IAddUserCommandHandler
{
    User Handle(AddUserCommand command);
}