using ApiOnlineShopWeb.Domain;
using ApiOnlineShopWeb.Database.Interfaces;
using Azure;
using System.Diagnostics.Metrics;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace ApiOnlineShopWeb.Database;

internal class UserRepository : IUserRepository
{
    private readonly ApiOnlineShopWebContext _context;

    public UserRepository(ApiOnlineShopWebContext context)
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

    public User? GetUserByEMail(string eMail)
    {
        return _context.User.FirstOrDefault(o => o.EMail == eMail);
    }

    public List<User> GetUserList()
    {
        return _context.User.ToList();
    }

    public void Update(User user)
    {
        var userToUpdate = _context.User.FirstOrDefault(o => o.Id == user.Id);

        userToUpdate.EMail = user.EMail;
        userToUpdate.GivenName = user.GivenName;
        userToUpdate.Surname = user.Surname;
        userToUpdate.Age = user.Age;
        userToUpdate.Country = user.Country;
        userToUpdate.City = user.City;
        userToUpdate.Street = user.Street;
        userToUpdate.HouseNumber = user.HouseNumber;
        userToUpdate.PostalCode = user.PostalCode;

        _context.SaveChanges();
    }

    public void AddUser(User user)
    {
        _context.User.Add(new User
        {
            EMail = user.EMail,
            GivenName = user.GivenName,
            Surname = user.Surname,
            Age = user.Age,
            Country = user.Country,
            City = user.City,
            Street = user.Street,
            HouseNumber = user.HouseNumber,
            PostalCode = user.PostalCode,
            Password = user.Password
        });

        _context.SaveChanges();
    }

    public void ChangePassword(int userId, string password)
    {
        var user = GetUserById(userId);
        user.Password = password;
        _context.SaveChanges();
    }
}
