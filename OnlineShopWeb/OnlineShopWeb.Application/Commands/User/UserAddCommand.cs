namespace OnlineShopWeb.Application.Commands.User;

public record UserAddCommand(string EMail, string GivenName, string Surname,
        int Age, string Country, string City, string Street,
        int HouseNumber, int PostalCode, string Password)
{
}
