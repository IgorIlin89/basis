using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineShopWeb.Domain;

public class UserService : IUserService
{
    private List<User> UserList = new List<User>
    {
        new User {
            UserId = 1,
            FirstName = "Igor",
            LastName = "Ilin",Age = 34,
            Location = new Location{
                Country = "Germany",
                City = "Hamburg",
                Street = "Fantasie Weg",
                PostalCode = 22763
            }},

        new User {
            UserId = 2,
            FirstName = "Yury",
            LastName = "Spiridonov",
            Age = 38,
            Location = new Location{
                Country = "Germany",
                City = "Hamburg",
                Street = "Habsburger Weg"
                ,PostalCode = 21364
            }},

        new User {
            UserId = 3,
            FirstName = "Dirk",
            LastName = "Esk",
            Age = 33,
            Location = new Location{Country = "Germany",
                City = "Hamburg",
                Street = "Straßburger Straße",
                PostalCode = 22324
            }}
    };

    public User? GetUser(int userId)
    {
        return UserList.Where(o => o.UserId == userId).FirstOrDefault();
    }
    public List<User> GetUserList() { return UserList; }

    public bool Delete(int userid)
    {
        var userToDelete = UserList.Where(o => o.UserId == userid).FirstOrDefault();
        return UserList.Remove(userToDelete);
    }

    public void Add(int userid, string firstName, string lastName, int Age, string country, string city, string street, int postalCode)
    {
        UserList.Add(new User
        {
            UserId = userid,
            FirstName = firstName,
            LastName = lastName,
            Age = Age,
            Location = new Location
            {
                Country = country,
                City = city,
                Street = street,
                PostalCode = postalCode
            }
        });
    }
}