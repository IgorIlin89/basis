﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Database.Interfaces;
using System.Reflection.Metadata.Ecma335;

namespace OnlineShopWeb.Controllers;

public class LoginController : Controller
{
    private readonly IUserRepository _userRepository;

    public LoginController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult SignIn()
    {

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            var user = _userRepository.GetUserByEMail(model.EMail);
            if (user is null)
            {
                ModelState.AddModelError("Model", "User does not exist");
                return View(model);
            }

            if (user.Password.Trim() != model.Password.Trim())
            {
                ModelState.AddModelError("Model", "Wrong Password");
                return View(model);
            }

            await SignIn(user);
            HttpContext.Session.SetInt32("UserId", user.Id);

        }
        return RedirectToAction("Index", "Product");
    }

    protected async Task SignIn(User user)
    {
        var claims = new[] {
            new Claim(ClaimTypes.Name, user.FirstName),
        };


        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);


        var authProperties = new AuthenticationProperties()
        {
            IsPersistent = true,
            AllowRefresh = true,
            ExpiresUtc = DateTimeOffset.Now.AddDays(1),
        };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProperties);
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        return RedirectToAction("SignIn", "Login");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Password == model.RepeatPassword)
            {
                _userRepository.AddUser(
                    new User
                    {
                        EMail = model.EMail,
                        Password = model.Password.Trim(),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Age = model.Age,
                        Country = model.Country,
                        City = model.City,
                        Street = model.Street,
                        HouseNumber = model.HouseNumber,
                        PostalCode = model.PostalCode
                    });
                await SignIn(_userRepository.GetUserByEMail(model.EMail));
                return RedirectToAction("Index", "User");
            }
            else
            {
                ModelState.AddModelError("Model", "The repeated Password was not the same");
                return View(model);
            }
        }
        return View(model);
    }

    [HttpGet]
    public IActionResult ChangePassword(int userId)
    {
        var model = new PasswordChangeModel
        {
            UserId = userId,
        };
        return View(model);
    }
    public IActionResult ChangePassword(PasswordChangeModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.OldPassword == _userRepository.GetUserById(model.UserId).Password && model.Password == model.RepeatPassword)
            {
                _userRepository.ChangePassword(model.UserId, model.Password);
                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("Model", "The old Password or the repeated Password was not correct");
                return View(model);
            }
            //model.OldPassword == _userRepository.GetUserById(model.UserId).Password ? _userRepository.ChangePassword(model.UserId, model.Password) : return View();
        }
        else
        {
            //ModelState.AddModelError("Model", "Fill in all fields");
            return View(model);
        }
    }
}
