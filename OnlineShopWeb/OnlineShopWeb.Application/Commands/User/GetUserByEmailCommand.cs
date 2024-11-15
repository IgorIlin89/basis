namespace OnlineShopWeb.Application.Commands.User;

public record GetUserByEmailCommand
{
    public readonly string EMail;

    public GetUserByEmailCommand(string email)
    {
        EMail = email;
    }
}
