using Microsoft.Extensions.Options;
using OnlineShopWeb.Domain;
using UserAdapter.DTOs;
using UserAdapter.Mapping;
using Utility.Misc;
using Utility.Misc.Options;

namespace UserAdapter;

public class UserAdapter : IUserAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string _apiUrl;

    public UserAdapter(IHttpClientWrapper httpClientWrapper,
        IOptions<ApiUserOptions> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _apiUrl = options.Value.ApiUrl;
    }

    public async Task<List<User>> GetUserList()
    {
        var received = await _httpClientWrapper.Get<List<UserDto>>(_apiUrl, "user", "list");
        return received.MapToUserList();
    }

    public void UserDelete(string id)
    {
        _httpClientWrapper.Delete(_apiUrl, "user", id);
    }

    public async Task<User> GetUserById(string id)
    {
        var received = await _httpClientWrapper.Get<UserDto>(_apiUrl, "user", id);
        return received.MapToUser();
    }

    public async Task<User> GetUserByEmail(User user)
    {
        var received = await _httpClientWrapper.Get<UserDto>(_apiUrl, "user", "email", user.MapToDto().EMail);

        var result = received.MapToUser();

        return result;
    }

    public async Task<User> UserUpdate(User userToUpdate)
    {
        var received = await _httpClientWrapper.Put<UserDto, UserDto>(_apiUrl, "user", userToUpdate.MapToDto());
        return received.MapToUser();
    }

    public async Task<User> UserAdd(User userToAdd)
    {
        var received = await _httpClientWrapper.Post<UserDto, UserDto>(_apiUrl, "user", userToAdd.MapToDto());
        var result = received.MapToUser();
        return result;
    }

    public async Task<User> ChangeUserPassword(ChangePasswordDto changePasswordDto)
    {
        var received = await _httpClientWrapper.Post<ChangePasswordDto, UserDto>(_apiUrl, "user", changePasswordDto, "changepassword");
        return received.MapToUser();
    }
}
