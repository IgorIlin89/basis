using ApiUser.Application.Commands;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers.Interfaces;

public interface IUpdateUserCommandHandler
{
    User Handle(UpdateUserCommand command);
}