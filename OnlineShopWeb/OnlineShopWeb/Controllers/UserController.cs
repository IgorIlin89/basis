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

    [HttpGet]
    public IActionResult Details(int id)
    {
        var user = _userService.GetUser(id);
        var model = new UserModel
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Age = user.Age, // If ? in user domain its error
            Location = new LocationModel {
                Country = user.Location.Country == null ? "default" : user.Location.Country,
                //City = user.Location.City.FirstOrDefault("test")
            }
        };
        
            //
        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var user = _userService.GetUser(id);
        var model = new UserModel
        {
            UserId = user.UserId,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Age = user.Age
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(UserModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _userService.GetUser(model.UserId);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Age = model.Age;
            user.Location.Country = model.Location.Country;

            return RedirectToAction("Index", "UserList");
        }
        else
        {
            return View(model);//css and sass, responsive ui framework bootstrap
        }
    }
}
