using OnlineShopWeb.Domain;
using UserAdapter.DTOs;

namespace UserAdapter;

public interface IUserAdapter
{
    Task<User> GetUserByEmail(User user);
    Task<User> GetUserById(string id);
    Task<User> UserAdd(User userToAdd);
    void UserDelete(string id);
    Task<List<User>> GetUserList();
    Task<User> UserUpdate(User userToUpdate);
    Task<User> ChangeUserPassword(ChangePasswordDto changePasswordDto);
}