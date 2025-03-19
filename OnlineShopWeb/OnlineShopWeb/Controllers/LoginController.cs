using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Models;
using System.Security.Claims;

namespace OnlineShopWeb.Controllers;

public class LoginController(IGetUserByEmailCommandHandler getUserByEmailCommandHandler,
    IUserAddCommandHandler userAddCommandHandler,
    IAuthenticationService authenticationService) : Controller
{
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
            var command = new GetUserByEmailCommand(model.EMail);
            var user = await getUserByEmailCommandHandler.Handle(command);

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
        var token = await authenticationService.GetTokenAsync(HttpContext,
            OpenIdConnectDefaults.AuthenticationScheme,
            "access_token");

        await HttpContext.SignOutAsync();
        return RedirectToAction("SignIn", "Login");
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
}
