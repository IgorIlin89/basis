using ApiUser.Application.Commands;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers.Interfaces;

public interface IGetUserByEmailCommandHandler
{
    User Handle(GetUserByEmailCommand command);
}