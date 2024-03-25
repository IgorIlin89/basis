using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class UserModel
{
    public int UserId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public int Age { get; set; }
    public required Location Location { get; set; }
}
