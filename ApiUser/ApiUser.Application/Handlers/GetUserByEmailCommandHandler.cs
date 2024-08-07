using ApiUser.Database.Interfaces;
using ApiUser.Domain;
using ApiUser.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiUser.Application.Handlers.Interfaces;

namespace ApiUser.Application.Handlers;

public class GetUserByEmailCommandHandler(IUnitOfWork UnitOfWork, IUserRepository Repository) : IGetUserByEmailCommandHandler
{
    public User Handle(GetUserByEmailCommand command)
    {
        return Repository.GetUserByEMail(command.Email);
    }
}
