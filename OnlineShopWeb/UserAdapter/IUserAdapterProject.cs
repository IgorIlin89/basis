using OnlineShopWeb.Domain;
using UserAdapter.DTOs;

namespace UserAdapter;

public interface IUserAdapterProject
{
    Task<User> GetUserByEmail(User user);
    Task<UserDto> GetUserById(string id);
    Task<User> UserAdd(User userToAdd);
    void UserDelete(string id);
    Task<List<User>> GetUserList();
    Task<UserDto> UserUpdate(UserDto userToUpdate);
    Task<ChangePasswordDto> ChangeUserPassword(ChangePasswordDto changePasswordDto);
}