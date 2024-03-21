using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class UsersController : Controller
{
    // GET: Users
    public IActionResult Index()
    {
        //UsersSingleton x = UsersSingleton.Instance;
        var model = new UsersModel
        {
            //User = UserService.Instance.GetUser(),
            //UserList = UserService.Instance.GetUserList()
        };
        return View(model);
    }
}
