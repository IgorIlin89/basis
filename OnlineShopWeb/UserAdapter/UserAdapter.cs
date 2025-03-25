using OnlineShopWeb.Domain;
using OnlineShopWeb.Domain.Commands;
using OnlineShopWeb.Domain.Interfaces;
using UserAdapter.DTOs;
using UserAdapter.Mapping;
using Utility.Misc;
using Utility.Misc.Options;

namespace UserAdapter;

internal class UserAdapter : IUserAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private (string ApiUrl, string ApiKey) _connectionData;

    public UserAdapter(IHttpClientWrapper httpClientWrapper,
        ApiUserOptions options)
    {
        _httpClientWrapper = httpClientWrapper;
        _connectionData.ApiUrl = options.ApiUrl;
        _connectionData.ApiKey = options.ApiKey;
    }

    public async Task<List<User>> GetUserList()
    {
        var received = await _httpClientWrapper.Get<List<UserDto>>(_connectionData, "user", "list");
        return received.MapToUserList();
    }

    public void UserDelete(string id)
    {
        _httpClientWrapper.Delete(_connectionData, "user", id);
    }

    public async Task<User> GetUserById(string id)
    {
        var received = await _httpClientWrapper.Get<UserDto>(_connectionData, "user", id);
        return received.MapToUser();
    }

    public async Task<User> GetUserByEmail(User user)
    {
        var received = await _httpClientWrapper.Get<UserDto>(_connectionData, "user", "email", user.MapToDto().EMail);

        var result = received.MapToUser();

        return result;
    }

    public async Task<User> UserUpdate(User userToUpdate)
    {
        var received = await _httpClientWrapper.Put<UserDto, UserDto>(_connectionData, "user", userToUpdate.MapToDto());
        return received.MapToUser();
    }

    public async Task<User> UserAdd(User userToAdd)
    {
        var received = await _httpClientWrapper.Post<UserDto, UserDto>(_connectionData, "user", userToAdd.MapToDto());
        var result = received.MapToUser();
        return result;
    }

    public async Task<User> ChangeUserPassword(ChangePasswordCommand changePasswordDto)
    {
        var received = await _httpClientWrapper.Post<ChangePasswordCommand, UserDto>(_connectionData, "user", changePasswordDto, "changepassword");
        return received.MapToUser();
    }
}
