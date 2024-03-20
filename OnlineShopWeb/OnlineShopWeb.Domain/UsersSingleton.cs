using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OnlineShopWeb.Domain;

// https://csharpindepth.com/articles/Singleton#:~:text=All%20you%20need%20to%20do,easily%20with%20a%20lambda%20expression.&text=It's%20simple%20and%20performs%20well,property%2C%20if%20you%20need%20that.
//sealed so no other class can inherit
sealed public class UsersSingleton
{
    /*public string Name { get; }
    public int Age { get; }
    public string Location {  get; }*/

    //Making constructor private to be singleton
    private UsersSingleton() { }
    //public UsersSingleton(string name, int age, string location) => (name, age, location) = (Name, Age, Location);
    public static UsersSingleton Instance { get { return NestedUsersSingleton.instance; } }

    private string User = "Igor";
    private List<string> UserList = new List<string> { "Igor", "Yury", "Billy","Dirk" };

    public string GetUser() { return User; }
    public List<string> GetUserList() { return UserList; }

    private class NestedUsersSingleton
    {
        static NestedUsersSingleton() { }
        internal static readonly UsersSingleton instance = new UsersSingleton();
    }

}