﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
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
        var userList = _userRepository.GetUserList();
        var model = new UserListModel();

        foreach (var user in userList)
        {
            model.UserModelList.Add(
                new UserModel
                {
                    UserId = user.Id,
                    EMail = user.EMail,
                    GivenName = user.GivenName,
                    Surname = user.Surname,
                    Age = user.Age,
                    Country = user.Country,
                    City = user.City,
                    Street = user.Street,
                    HouseNumber = user.HouseNumber,
                    PostalCode = user.PostalCode,
                });
        }

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
        var user = _userRepository.GetUserById(id);

        var model = new UserModel
        {
            UserId = user.Id,
            EMail = user.EMail.Trim(),
            GivenName = user.GivenName.Trim(),
            Surname = user.Surname.Trim(),
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

        if (HttpContext.User.Identity.Name is not null)
        {
            var user = _userRepository.GetUserById(Int32.Parse(HttpContext.User.Identity.Name));

            model.UserId = user.Id;
            model.EMail = user.EMail.Trim();
            model.GivenName = user.GivenName.Trim();
            model.Surname = user.Surname.Trim();
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
            _userRepository.EditUser(
                new User
                {
                    Id = model.UserId.Value,
                    EMail = model.EMail,
                    GivenName = model.GivenName,
                    Surname = model.Surname,
                    Age = model.Age,
                    Country = model.Country,
                    City = model.City,
                    Street = model.Street,
                    HouseNumber = model.HouseNumber,
                    PostalCode = model.PostalCode
                }
            );

            return RedirectToAction("Index", "User");
        }
        else
        {
            return View(model);
        }
    }
}
