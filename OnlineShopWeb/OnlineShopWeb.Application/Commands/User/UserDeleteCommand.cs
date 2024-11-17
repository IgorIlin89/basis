namespace OnlineShopWeb.Application.Commands.User;

public record UserDeleteCommand
{
    public readonly string UserId;

    public UserDeleteCommand(string userId)
    {
        UserId = userId;
    }
}
