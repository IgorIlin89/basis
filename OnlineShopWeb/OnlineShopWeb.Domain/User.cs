using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Domain;

public class User
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public Location? Location { get; set; }
}
