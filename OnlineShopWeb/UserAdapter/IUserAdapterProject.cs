using OnlineShopWeb.Domain;
using UserAdapter.DTOs;

namespace UserAdapter;

public interface IUserAdapterProject
{
    Task<User> GetUserByEmail(User user);
    Task<UserDto> GetUserById(string id);
    Task<UserDto> UserAdd(UserDto userToAdd);
    void UserDelete(string id);
    Task<List<UserDto>> GetUserList();
    Task<UserDto> UserUpdate(UserDto userToUpdate);
    Task<ChangePasswordDto> ChangeUserPassword(ChangePasswordDto changePasswordDto);
}