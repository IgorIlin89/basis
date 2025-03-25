namespace OnlineShopWeb.Domain.Commands;

public class ChangePasswordCommand
{
    public string UserId { get; set; }
    public string Password { get; set; }
}
