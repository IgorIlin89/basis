using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database.Interfaces;

public interface IUserRepository
{
    List<User> GetUserList();

    User? GetUserById(int id);
    User? GetUserByName(string firstName);
    public User? GetUserByEMail(string eMail);

    void AddUser(User user);

    void DeleteUser(int id);
    void EditUser(User user);
    public void ChangePassword(int userId, string password);
}