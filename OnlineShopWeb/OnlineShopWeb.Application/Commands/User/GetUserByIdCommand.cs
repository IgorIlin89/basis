namespace OnlineShopWeb.Application.Commands.User;

public record GetUserByIdCommand
{
    public readonly string UserId;

    public GetUserByIdCommand(string userId)
    {
        UserId = userId;
    }
}
