using ApiUser.Domain;

namespace ApiUser.Database.Interfaces;

public interface IUserRepository
{
    List<User> GetUserList();
    void Delete(int id);
}