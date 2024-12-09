namespace OnlineShopWeb.Application.Commands.User;

public record ChangeUserPasswordCommand(string UserId, string Password)
{
}
