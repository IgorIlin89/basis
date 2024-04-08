using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Models;

public class UserModel
{
    [Required] //that is an attribute, only for model
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public LocationModel? Location { get; set; }
}
