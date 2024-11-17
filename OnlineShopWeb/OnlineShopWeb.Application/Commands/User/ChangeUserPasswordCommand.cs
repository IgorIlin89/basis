namespace OnlineShopWeb.Application.Commands.User;

public record ChangeUserPasswordCommand
{
    public readonly string UserId;
    public readonly string Password;

    public ChangeUserPasswordCommand(string userId, string password)
    {
        UserId = userId;
        Password = password;
    }
}
