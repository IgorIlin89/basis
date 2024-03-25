using System.ComponentModel.DataAnnotations;

namespace OnlineShopWeb.Domain;

public class User
{
    public User() { }
    public int UserId { get; set; }
    [Required] //that is an attribute, only for model
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Age { get; set; }
    public Location? Location { get; set; }

    public User(int userId, string firstName, string lastName, int age, Location location)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Location = location;
    }

}
