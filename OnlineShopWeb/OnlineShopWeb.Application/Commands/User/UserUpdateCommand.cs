namespace OnlineShopWeb.Application.Commands.User;

public record UserUpdateCommand(string UserId, string EMail, string GivenName, string Surname,
        int Age, string Country, string City, string Street,
        int HouseNumber, int PostalCode)
{
}
