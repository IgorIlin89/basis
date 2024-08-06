using ApiUser.Database.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUser.Database;

public class UnitOfWork(ApiUserContext DbContext) : IUnitOfWork
{
    public void SaveChanges()
    {
        DbContext.SaveChanges();
    }
}
