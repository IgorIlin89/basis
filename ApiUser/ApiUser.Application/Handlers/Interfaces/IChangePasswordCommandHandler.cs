using ApiUser.Application.Commands;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers.Interfaces;

public interface IChangePasswordCommandHandler
{
    User Handle(ChangePasswordCommand command);
}