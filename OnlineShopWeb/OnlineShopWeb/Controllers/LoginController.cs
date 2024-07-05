﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Database.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using OnlineShopWeb.Dtos;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace OnlineShopWeb.Controllers;

public class LoginController : Controller
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly IUserRepository _userRepository;
    private readonly string _connectionString;
    private readonly string _connectToGetUserByEMail;
    private readonly string _connectToAddUser;

    public LoginController(IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _connectionString = configuration.GetConnectionString("ApiURL");
        _connectToGetUserByEMail = configuration.GetConnectionString("ApiUserControllerGetUserByEmail");
        _connectToAddUser = configuration.GetConnectionString("ApiUserControllerAddUser");
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
            var loginDto = new LoginDto
            {
                EMail = model.EMail,
                Password = model.Password
            };

            var httpBody = new StringContent(
                    JsonSerializer.Serialize(loginDto),
                    Encoding.UTF8,
                    Application.Json
                    );

            var request = await _httpClient.PostAsync(_connectionString + _connectToGetUserByEMail, httpBody);
            var response = await request.Content.ReadAsStringAsync();

            var userDto = JsonSerializer.Deserialize<UserDto>(response);

            var user = new User
            {
                Id = userDto.UserId.Value,
                EMail = userDto.EMail,
                GivenName = userDto.GivenName,
                Surname = userDto.Surname,
                Age = userDto.Age,
                Country = userDto.Country,
                City = userDto.City,
                Street = userDto.Street,
                HouseNumber = userDto.HouseNumber,
                PostalCode = userDto.PostalCode,
                Password = userDto.Password
            };

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

        }
        return RedirectToAction("Index", "Product");
    }

    protected async Task SignIn(User user)
    {
        var claims = new[] {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.GivenName, user.GivenName),
            new Claim(ClaimTypes.Surname, user.Surname),
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
                if (_userRepository.GetUserByEMail(model.EMail) is null)
                {
                    var userToAdd = new User
                    {
                        EMail = model.EMail,
                        Password = model.Password.Trim(),
                        GivenName = model.FirstName,
                        Surname = model.LastName,
                        Age = model.Age,
                        Country = model.Country,
                        City = model.City,
                        Street = model.Street,
                        HouseNumber = model.HouseNumber,
                        PostalCode = model.PostalCode
                    };

                    var httpBody = new StringContent(
                    JsonSerializer.Serialize(userToAdd),
                    Encoding.UTF8,
                    Application.Json
                    );

                    var request = await _httpClient.PostAsync(_connectionString + _connectToAddUser, httpBody);

                    await SignIn(_userRepository.GetUserByEMail(model.EMail));
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    ModelState.AddModelError("Model", "The E-Mail adress is already taken");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError("Model", "The repeated Password was not the same");
                return View(model);
            }
        }
        return View(model);
    }
}
