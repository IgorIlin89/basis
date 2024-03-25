using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }
    
    public IActionResult Details(int id)
    {
        var user = _userService.GetUser(id);
        return View(user);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _userService.GetUser(id);
        return View(user);
    }

    [HttpPost]
    public IActionResult Edit(User model) //TODO: UserModel
    {
        if (ModelState.IsValid)
        {
            var user = _userService.GetUser(model.UserId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Age = model.Age;
            //user.Location.Country = model.Location.Country;

            return RedirectToAction("Index", "UserList");
        }
        else
        {
            return View(model);//css and sass, responsive ui framework bootstrap
        }
    }
}
