using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database.Interfaces;

public interface IUserRepository
{
    List<User> GetUserList();

    User? GetUserById(int id);
    User? GetUserByName(string firstName);
    public User? GetUserByEMail(string eMail);

    public bool CheckUserPassword(string password);

    void AddUser(User user);

    void DeleteUser(int id);
    void EditUser(User user);
}