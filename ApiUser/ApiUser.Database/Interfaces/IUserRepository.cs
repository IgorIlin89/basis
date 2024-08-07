using ApiUser.Domain;

namespace ApiUser.Database.Interfaces;

public interface IUserRepository
{
    User AddUser(User user);
    void Delete(int id);
    User? GetUserById(int id);
    User? GetUserByEMail(string email);
    List<User> GetUserList();
    User Update(User user);
    public User ChangePassword(int id, string password);
}