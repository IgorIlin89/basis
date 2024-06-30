using OnlineShopWebAPI.Domain;

namespace OnlineShopWebAPI.Database.Interfaces;

public interface IUserRepository
{
    void AddUser(User user);
    void Delete(int id);
    User? GetUserById(int id);
    List<User> GetUserList();
    void Update(User user);
}