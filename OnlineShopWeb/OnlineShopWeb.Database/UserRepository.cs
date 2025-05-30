﻿using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database;

internal class UserRepository : IUserRepository
{
    private OnlineShopWebDbContext _dbContext;

    public UserRepository(OnlineShopWebDbContext onlineShopWebDbContext)
    {
        _dbContext = onlineShopWebDbContext;
    }
    public void AddUser(User user)
    {
        _dbContext.User.Add(user);
        _dbContext.SaveChanges();

    }

    public void DeleteUser(int id)
    {
        var entityEntry = GetUserById(id);
        _dbContext.Remove(entityEntry);
        _dbContext.SaveChanges();
    }

    public void EditUser(User user)
    {
        var entityEntry = GetUserById(user.Id);
        entityEntry.GivenName = user.GivenName;
        entityEntry.Surname = user.Surname;
        entityEntry.Age = user.Age;
        entityEntry.Country = user.Country;
        entityEntry.City = user.City;
        entityEntry.Street = user.Street;
        entityEntry.HouseNumber = user.HouseNumber;
        entityEntry.PostalCode = user.PostalCode;

        _dbContext.SaveChanges();
    }

    public User? GetUserById(int id)
    {
        return _dbContext.User.FirstOrDefault(o => o.Id == id);
    }

    public User? GetUserByName(string firstName)
    {
        return _dbContext.User.FirstOrDefault(o => o.GivenName == firstName);
    }

    public User? GetUserByEMail(string eMail)
    {
        return _dbContext.User.FirstOrDefault(o => o.EMail == eMail);
    }

    public List<User> GetUserList()
    {
        return _dbContext.User.ToList();
    }

    public void ChangePassword(int userId, string password)
    {
        var user = GetUserById(userId);
        user.Password = password;
        _dbContext.SaveChanges();
    }


}
