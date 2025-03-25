using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using System.Security.Claims;
using Utility.Misc;

namespace OnlineShopWeb.Controllers;

public class LoginController(IGetUserByEmailCommandHandler getUserByEmailCommandHandler,
    IUserAddCommandHandler userAddCommandHandler,
    IAuthenticationService authenticationService,
    IHttpClientWrapper httpClientWrapper) : Controller
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

        var redirectUri = "https://localhost:7195";

        return Redirect($"https://localhost:7073/connect/endsession?id_token_hint={token}&post_logout_redirect_uri={redirectUri}");
    }

    [AllowAnonymous]
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(RegistrationModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.Password == model.RepeatPassword)
            {

                var command = new UserAddCommand(model.EMail, model.FirstName, model.LastName, model.Age,
                    model.Country, model.City, model.Street, model.HouseNumber, model.PostalCode, model.Password);

                var userToLogin = await userAddCommandHandler.Handle(command);

                //await SignIn(userToLogin);

                return RedirectToAction("Index", "Home");
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
