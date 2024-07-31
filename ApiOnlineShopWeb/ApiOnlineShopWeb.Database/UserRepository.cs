using ApiOnlineShopWeb.Domain;
using ApiOnlineShopWeb.Database.Interfaces;
using Azure;
using System.Diagnostics.Metrics;
using System.IO;
using Microsoft.EntityFrameworkCore;
using ApiOnlineShopWeb.Domain.Exceptions;

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

        if (userToUpdate is null)
        {
            throw new NotFoundException($"User with the Id {user.Id} could not be found and updated");
        }

        if (userToUpdate is null)

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

    public User AddUser(User user)
    {
        var existingUser = _context.User.FirstOrDefault(x => x.EMail == user.EMail);

        if(existingUser != null)
        {
            throw new UserExistsException($"User with email '{user.EMail}' exists");
        }

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

        return user;
    }

    public void ChangePassword(int userId, string password)
    {
        var user = GetUserById(userId);

        if (user is null)
        {
            throw new NotFoundException($"Password of user {userId} could not be changed. User not found");
        }

        user.Password = password;
        _context.SaveChanges();
    }
}
