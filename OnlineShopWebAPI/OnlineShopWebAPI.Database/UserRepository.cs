using OnlineShopWebAPI.Database.Interfaces;
using OnlineShopWebAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWebAPI.Database;

public class UserRepository : IUserRepository
{
    private readonly OnlineShopWebAPIContext _context;

    public UserRepository(OnlineShopWebAPIContext context)
    {
        _context = context;
    }

    public void Delete(int id)
    {
        var user = _context.User.FirstOrDefault(o => o.Id == id);
        _context.User.Remove(user);
        _context.SaveChanges();
    }

    public User? GetUserById(int id)
    {
        var user = _context.User.FirstOrDefault(o => o.Id == id);
        return user;
    }

    public List<User> GetUserList()
    {
        return _context.User.ToList();
    }

    public void Update(User user)
    {
        var userToUpdate = _context.User.FirstOrDefault(o => o.Id == user.Id);

        userToUpdate.EMail = user.EMail;
        //userToUpdate.Name = user.Name;
        //userToUpdate.Password = user.Password;

        _context.SaveChanges();
    }

    public void AddUser(User user)
    {
        _context.User.Add(new User
        {
            EMail = user.EMail,
            Password = user.Password,
            Name = user.Name
        });

        _context.SaveChanges();
    }
}
