using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiUser.Database.Interfaces;
using ApiUser.Domain;

namespace ApiUser.Database;

internal class UserRepository : IUserRepository
{
    private readonly ApiUserContext _context;

    public UserRepository(ApiUserContext context)
    {
        _context = context;
    }

    public List<User> GetUserList()
    {
        return _context.User.ToList();
    }

    public void Delete(int id)
    {
        var user = _context.User.FirstOrDefault(o => o.Id == id);
        _context.User.Remove(user);
        _context.SaveChanges();
    }
}
