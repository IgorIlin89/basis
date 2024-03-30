﻿namespace OnlineShopWeb.Domain;

public class Location
{
    public string Country { get; set; }
    public string City { get; set; }
    public string? District { get; set; }
    public string Street { get; set; }
    public int PostalCode { get; set; }

    public Location(string country, string city, string district, string street, int postalCode)
    {
        Country = country;
        City = city;
        District = district;
        Street = street;
        PostalCode = postalCode;
    }
}
