namespace OnlineShopWeb.Models;

public class LocationModel
{
    public required string Country { get; set; }
    public required string City { get; set; }
    public string? District { get; set; }
    public required string Street { get; set; }
    public int PostalCode { get; set; }
}
