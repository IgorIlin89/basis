using Microsoft.Extensions.Options;
using OnlineShopWeb.Domain;
using UserAdapter.DTOs;
using Utility.Misc;

namespace UserAdapter;

public class UserAdapterProject : IUserAdapterProject
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string _apiUrl;

    public UserAdapterProject(IHttpClientWrapper httpClientWrapper,
        IOptionsSnapshot<HttpClientWrapperOptions> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _apiUrl = options.Get("ApiUserClientOptions").ApiUrl;
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

    public async Task<UserDto> GetUserById(string id)
    {
        return await _httpClientWrapper.Get<UserDto>(_apiUrl, "user", id);
    }

    public async Task<User> GetUserByEmail(User user)
    {
        var request = user.MapToDto();
        var received = await _httpClientWrapper.Get<UserDto>(_apiUrl, "user", "email", request.EMail);

        var result = received.MapToUser();

        return result;
    }

    public async Task<UserDto> UserUpdate(UserDto userToUpdate)
    {
        return await _httpClientWrapper.Put<UserDto, UserDto>(_apiUrl, "user", userToUpdate);
    }

    public async Task<User> UserAdd(User userToAdd)
    {
        var request = userToAdd.MapToDto();
        var received = await _httpClientWrapper.Post<UserDto, UserDto>(_apiUrl, "user", request);
        var result = received.MapToUser();
        return result;
    }

    public async Task<ChangePasswordDto> ChangeUserPassword(ChangePasswordDto changePasswordDto)
    {
        return await _httpClientWrapper.Post<ChangePasswordDto, ChangePasswordDto>(_apiUrl, "user", changePasswordDto, "changepassword");
    }
}
