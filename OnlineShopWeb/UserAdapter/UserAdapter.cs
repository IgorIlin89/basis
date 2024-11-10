namespace UserAdapter;

public class UserAdapter : IUserAdapter
{
    private readonly IHttpClientWrapper _httpClientWrapper;
    private readonly string _apiUrl;

    public UserAdapter(IHttpClientWrapper httpClientWrapper,
        IOptionsSnapshot<HttpClientWrapperOptions> options)
    {
        _httpClientWrapper = httpClientWrapper;
        _apiUrl = options.Get("ApiUserClientOptions").ApiUrl;
    }

    public async Task<List<UserDto>> GetUserList()
    {
        return await _httpClientWrapper.Get<List<UserDto>>(_apiUrl, "user", "list");
    }

    public void UserDelete(string id)
    {
        _httpClientWrapper.Delete(_apiUrl, "user", id);
    }

    public async Task<UserDto> GetUserById(string id)
    {
        return await _httpClientWrapper.Get<UserDto>(_apiUrl, "user", id);
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
        return await _httpClientWrapper.Get<UserDto>(_apiUrl, "user", "email", email);
    }

    public async Task<UserDto> UserUpdate(UserDto userToUpdate)
    {
        return await _httpClientWrapper.Put<UserDto, UserDto>(_apiUrl, "user", userToUpdate);
    }

    public async Task<UserDto> UserAdd(UserDto userToAdd)
    {
        return await _httpClientWrapper.Post<UserDto, UserDto>(_apiUrl, "user", userToAdd);
    }

    public async Task<ChangePasswordDto> ChangeUserPassword(ChangePasswordDto changePasswordDto)
    {
        return await _httpClientWrapper.Post<ChangePasswordDto, ChangePasswordDto>(_apiUrl, "user", changePasswordDto, "changepassword");
    }
}
