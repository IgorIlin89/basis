using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Database.Interfaces;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

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
                    _userRepository.AddUser(
                        new User
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
                        });
                    await SignIn(_userRepository.GetUserByEMail(model.EMail));
                    return RedirectToAction("Index", "User");
                }
                else {
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
