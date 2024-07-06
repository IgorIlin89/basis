using OnlineShopWeb.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;
using OnlineShopWeb.Dtos;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;

namespace OnlineShopWeb.Controllers;

public class PasswordController : Controller
{
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _connectionString;
    private readonly string _connectToChangePassword;
    private readonly string _connectToGetUserById;

    public PasswordController(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("ApiURL");
        _connectToChangePassword = configuration.GetConnectionString("ApiUserControllerChangePassword");
        _connectToGetUserById = configuration.GetConnectionString("ApiUserControllerGetUserById");
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
            var requestUser = await _httpClient.GetAsync(_connectionString + _connectToGetUserById + model.UserId);
            var responseUser = await requestUser.Content.ReadAsStringAsync();

            var userDto = JsonSerializer.Deserialize<UserDto>(responseUser);

            if (model.OldPassword == userDto.Password && model.Password == model.RepeatPassword)
            {
                var changePasswordDto = new ChangePasswordDto
                {
                    UserId = model.UserId,
                    Password = model.Password
                };

                var httpBody = new StringContent(
                    JsonSerializer.Serialize(changePasswordDto),
                    Encoding.UTF8,
                    Application.Json
                    );

                var request = _httpClient.PostAsync(_connectionString + _connectToChangePassword, httpBody);
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
