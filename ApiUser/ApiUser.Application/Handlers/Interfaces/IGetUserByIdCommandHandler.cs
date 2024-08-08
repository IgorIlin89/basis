using ApiUser.Application.Commands;
using ApiUser.Domain;

namespace ApiUser.Application.Handlers.Interfaces;

public interface IGetUserByIdCommandHandler
{
    User Handle(GetUserByIdCommand command);
}