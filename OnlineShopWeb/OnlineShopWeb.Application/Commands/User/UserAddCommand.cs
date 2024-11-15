namespace OnlineShopWeb.Application.Commands.User;

public record UserAddCommand
{
    public readonly string EMail;
    public readonly string GivenName;
    public readonly string Surname;
    public readonly int Age;
    public readonly string Country;
    public readonly string City;
    public readonly string Street;
    public readonly int HouseNumber;
    public readonly int PostalCode;
    public readonly string Password;

    public UserAddCommand(string email, string givenName, string surname,
        int age, string country, string city, string street,
        int housenumber, int postalCode, string password)
    {
        EMail = email;
        GivenName = givenName;
        Surname = surname;
        Age = age;
        Country = country;
        City = city;
        Street = street;
        HouseNumber = housenumber;
        PostalCode = postalCode;
        Password = password;
    }
}
