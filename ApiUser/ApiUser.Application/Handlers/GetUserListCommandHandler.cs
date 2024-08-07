using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Database.Interfaces;
using ApiUser.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUser.Application.Handlers;

public class GetUserListCommandHandler(IUnitOfWork UnitOfWork, IUserRepository Repository) : IGetUserListCommandHandler
{
    public List<User> Handle()
    {
        return Repository.GetUserList();
    }
}
