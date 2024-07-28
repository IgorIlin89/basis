using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using OnlineShopWeb.Domain;
using System.Text.Json;
using OnlineShopWeb.Dtos;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using System.Net;
using OnlineShopWeb.Misc;

namespace OnlineShopWeb.Controllers;

public class LoginController : Controller
{
    IHttpClientWrapper _httpClientWrapper;

    public LoginController(IHttpClientWrapper clientWrapper)
    {
        _httpClientWrapper = clientWrapper;
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
            var userDto = await _httpClientWrapper.Get<UserDto>("user", "email", model.EMail);

            if (userDto is null)
            {
                ModelState.AddModelError("Model", "User does not exist");
                return View(model);
            }

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
                var userDto = await _httpClientWrapper.Get<UserDto>("user", "email", model.EMail);

                if (userDto is null)
                {
                    var userToAdd = new UserDto
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

                    var request = await _httpClientWrapper.Post<UserDto>("user", userToAdd);

                    var newUserWithId = await _httpClientWrapper.Get<UserDto>("user", "email", model.EMail);

                    var userToLogin = new User
                    {
                        Id = newUserWithId.UserId.Value,
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

                    await SignIn(userToLogin);
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
