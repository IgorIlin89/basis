namespace OnlineShopWeb.Application.Commands;

public record GetUserByEmailCommand
{
    public string EMail;

    public GetUserByEmailCommand(string email)
    {
        EMail = email;
    }
}
