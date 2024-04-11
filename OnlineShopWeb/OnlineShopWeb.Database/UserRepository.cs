using OnlineShopWeb.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        var entityEntry = GetUser(id);
        _dbContext.Remove(entityEntry);
        _dbContext.SaveChanges();
    }

    public void EditUser(User user)
    {
        var entityEntry = GetUser(user.Id);
        entityEntry.Id = user.Id;
        entityEntry.FirstName = user.FirstName;
        entityEntry.LastName = user.LastName;
        entityEntry.Age = user.Age;
        entityEntry.Country = user.Country;
        entityEntry.City = user.City;
        entityEntry.Street = user.Street;
        entityEntry.HouseNumber = user.HouseNumber;
        entityEntry.PostalCode = user.PostalCode;

        _dbContext.SaveChanges();
    }

    public User? GetUser(int id)
    {
        return _dbContext.User.FirstOrDefault(o => o.Id == id);
    }

    public List<User> GetUserList()
    {
        return _dbContext.User.ToList();
    }
}
