using OnlineShopWeb.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Models;
using OnlineShopWeb.Dtos;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using OnlineShopWeb.Misc;

namespace OnlineShopWeb.Controllers;

public class PasswordController : Controller
{
    public IHttpClientWrapper _httpClientWrapper;
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _connectionString;
    private readonly string _connectToChangePassword;
    private readonly string _connectToGetUserById;

    public PasswordController(IHttpClientWrapper clientWrapper)
    {
        _httpClientWrapper = clientWrapper;
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        var model = new PasswordChangeModel
        {
            UserId = HttpContext.Name(),
        };

        return View("Views/User/ChangePassword.cshtml", model);
    }

    [HttpPost]
    public async Task<ActionResult> ChangePassword(PasswordChangeModel model)
    {
        if (ModelState.IsValid)
        {
            var userDto = await _httpClientWrapper.Get<UserDto>("user", model.UserId.ToString());

            if (model.OldPassword == userDto.Password && model.Password == model.RepeatPassword)
            {
                var changePasswordDto = new ChangePasswordDto
                {
                    UserId = model.UserId,
                    Password = model.Password
                };

                var request = await _httpClientWrapper.Post<ChangePasswordDto, ChangePasswordDto>("user", changePasswordDto, "changepassword");

                return RedirectToAction("Index", "Product");
            }
            else
            {
                ModelState.AddModelError("Model", "The old Password or the repeated Password was not correct");
                return View("Views/User/ChangePassword.cshtml", model);
            }
        }
        else
        {
            return View("Views/User/ChangePassword.cshtml", model);
        }
    }
}
