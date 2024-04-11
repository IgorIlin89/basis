using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using System.Diagnostics;
using System.Reflection;

namespace OnlineShopWeb.Controllers;

public class UserController : Controller
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new UserListModel
        {
            UserList = _userRepository.GetUserList()
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _userRepository.DeleteUser(id);
        return RedirectToAction("Index", "User");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var user = _userRepository.GetUser(id);

        var model = new UserModel
        {
            UserId = user.Id,
            FirstName = user.FirstName.Trim(),
            LastName = user.LastName.Trim(),
            Age = user.Age,
            Country = user.Country.Trim(),
            City = user.City.Trim(),
            Street = user.Street.Trim(),
            HouseNumber = user.HouseNumber,
            PostalCode = user.PostalCode
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Update(int? id)
    {
        var model = new UserModel();

        if (id is not null)
        {
            var user = _userRepository.GetUser(id.Value);

            model.UserId = user.Id;
            model.FirstName = user.FirstName.Trim();
            model.LastName = user.LastName.Trim();
            model.Age = user.Age;
            model.Country = user.Country.Trim();
            model.City = user.City.Trim();
            model.Street = user.Street.Trim();
            model.HouseNumber = user.HouseNumber;
            model.PostalCode = user.PostalCode;

        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Update(UserModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.UserId is not null)
            {
                _userRepository.EditUser(
                    new User
                    {
                        Id = model.UserId.Value,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Age = model.Age,
                        Country = model.Country,
                        City = model.City,
                        Street = model.Street,
                        HouseNumber = model.HouseNumber,
                        PostalCode = model.PostalCode
                    }
                );
            }
            else
            {
                _userRepository.AddUser(
                    new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Age = model.Age,
                        Country = model.Country,
                        City = model.City,
                        Street = model.Street,
                        HouseNumber = model.HouseNumber,
                        PostalCode = model.PostalCode
                    }
                );

            }
            return RedirectToAction("Index", "User");
        }
        else
        {
            return View(model);
        }
    }
}
