using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models;

public class UserModel
{
    public int? UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public LocationModel? Location { get; set; }
}
