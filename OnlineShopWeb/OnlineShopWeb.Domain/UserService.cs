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
        new User(0,"Igor","Ilin",34,new Location("Germany","Hamburg", "Rahlstedt","Fantasie Weg",22763)),
        new User(1,"Yury","Spiridonov", 38, new Location("Germany", "Hamburg", "Harburg","Habsburger Weg",21364)),
        new User(2,"Dirk","Esk", 33, new Location("Germany", "Hamburg", "Altona","Straßburger Straße",22324))
    };

    public User? GetUser(int userId)
    {
        return UserList.Where(o => o.UserId == userId).FirstOrDefault();
    }
    public List<User> GetUserList() { return UserList; }
}