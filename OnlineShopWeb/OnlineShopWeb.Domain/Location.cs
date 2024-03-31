namespace OnlineShopWeb.Domain;

public class Location
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Street { get; set; }
    public int PostalCode { get; set; }

    public Location(string country, string city, string street, int postalCode)
    {
        Country = country;
        City = city;
        Street = street;
        PostalCode = postalCode;
    }
}
