using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database;

public interface IUserRepository
{
    List<User> GetUserList();

    User? GetUserById(int id);
    User? GetUserByName(string firstName);

    void AddUser(User user);

    void DeleteUser(int id);
    void EditUser(User user);
}