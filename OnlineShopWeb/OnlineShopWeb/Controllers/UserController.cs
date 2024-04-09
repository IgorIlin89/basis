﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using System.Diagnostics;
using System.Reflection;

namespace OnlineShopWeb.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var model = new UserListModel
        {
            UserList = _userService.GetUserList()
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

        if (id != null)
        {
            var user = _userService.GetUser(id.Value);


            model.UserId = user.UserId;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Age = user.Age;
            model.Location = new LocationModel
            {
                Country = user.Location.Country,
                City = user.Location.City,
                Street = user.Location.Street,
                PostalCode = user.Location.PostalCode
            };
        }

        return View(model);
    }

    [HttpPost]
    public IActionResult Update(UserModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _userService.GetUser(model.UserId.GetValueOrDefault());

            if (user != null)
            {
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
            _userService.Add(_userService.GetUserList().Count +1, 
                model.FirstName,
                model.LastName, 
                model.Age, 
                model.Location.Country,
                model.Location.City, 
                model.Location.Street,
                model.Location.PostalCode);

            }
            return RedirectToAction("Index", "User");
        }
        else
        {
            return View(model);
        }
    }
}
