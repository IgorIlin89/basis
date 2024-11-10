namespace UserAdapter;

public interface IUserAdapter
{
    Task<UserDto> GetUserByEmail(string email);
    Task<UserDto> GetUserById(string id);
    Task<UserDto> UserAdd(UserDto userToAdd);
    void UserDelete(string id);
    Task<List<UserDto>> GetUserList();
    Task<UserDto> UserUpdate(UserDto userToUpdate);
    Task<ChangePasswordDto> ChangeUserPassword(ChangePasswordDto changePasswordDto);
}