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
    private List<User> UserList = new List<User> {
        new User(1,"Igor","Ilin",34,new Location("Germany","Hamburg","Fantasie Weg",22763)),
        new User(2,"Yury","Spiridonov", 38, new Location("Germany", "Hamburg","Habsburger Weg",21364)),
        new User(3,"Dirk","Esk", 33, new Location("Germany", "Hamburg","Straßburger Straße",22324))
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
        UserList.Add(new User(userid, firstName, lastName, Age, new Location(country, city, street, postalCode)));
    }
}