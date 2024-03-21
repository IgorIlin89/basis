using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineShopWeb.Domain;

public class UserService : IUserService
// getrennte singletons fuer user und fuer users
{
    private string _userId = "Igor";
    private List<string> UserList = new List<string> { "Igor", "Yury", "Billy", "Dirk" };

    public string GetUser() { return _userId; }
    public List<string> GetUserList() { return UserList; }
}