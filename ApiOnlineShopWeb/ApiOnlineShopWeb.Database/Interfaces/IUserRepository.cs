using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Database.Interfaces;

public interface IUserRepository
{
    void AddUser(User user);
    void Delete(int id);
    User? GetUserById(int id);
    User? GetUserByEMail(string eMail);
    List<User> GetUserList();
    void Update(User user);
    public void ChangePassword(int userId, string password);
}