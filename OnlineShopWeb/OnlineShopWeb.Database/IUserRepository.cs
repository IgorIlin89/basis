using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database;

public interface IUserRepository
{
    List<User> GetUserList();

    User? GetUser(int id);

    void AddUser(User user);

    void DeleteUser(int id);
    void EditUser(User user);
}