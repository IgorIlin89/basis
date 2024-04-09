using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using System.Diagnostics;
using System.Reflection;

namespace OnlineShopWeb.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ISampleRepository _sampleRepository;

    public UserController(IUserService userService,
        ISampleRepository sampleRepository)
    {
        _userService = userService;
        _sampleRepository = sampleRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var customers = _sampleRepository.GetCustomers();
        var userList = new List<User>();
        foreach(var customer in customers)
        {
            userList.Add(new User(
                customer.Id,
                customer.Name,
                customer.Name,
                0,
                new Location("", "", "", 12345)
            ));
        }

        var model = new UserListModel
        {
            UserList = userList
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return RedirectToAction("Index", "User");
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
            Age = user.Age,
            Location = new LocationModel
            {
                Country = user.Location.Country,
                City = user.Location.City,
                Street = user.Location.Street,
                PostalCode = user.Location.PostalCode
            }
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Update(int? id)
    {
        var model = new UserModel();

        if (id is not null)
        {
            //var user = _userService.GetUser(id.Value);
            var customer = _sampleRepository.GetCustomer(id.Value);

            //TODO
            if(customer is null)
            {
                //todo logging
            }

            model.UserId = customer.Id;
            model.FirstName = customer.Name;
            model.LastName = customer.Name;
            model.Age = 0;
            model.Location = new LocationModel
            {
                //Country = "",
                //City = "",
                //Street = "",
                //PostalCode = ""
            };
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
                var user = _userService.GetUser(model.UserId.Value);

                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Age = model.Age;
                user.Location.Country = model.Location.Country;
                user.Location.City = model.Location.City;
                user.Location.Street = model.Location.Street;
                user.Location.PostalCode = model.Location.PostalCode;
            }
            else
            {
                _sampleRepository.AddCustomer(new Customer
                {
                    Name = model.FirstName
                });

            }
            return RedirectToAction("Index", "User");
        }
        else
        {
            return View(model);
        }
    }
}
