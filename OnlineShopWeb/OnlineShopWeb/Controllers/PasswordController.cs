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
    private readonly IUserRepository _userRepository;
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _connectionString;
    private readonly string _connectToChangePassword;

    public PasswordController(IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _connectionString = configuration.GetConnectionString("ApiURL");
        _connectToChangePassword = configuration.GetConnectionString("ApiUserControllerChangePassword");
    }

    [HttpGet]
    public IActionResult ChangePassword()
    {
        var model = new PasswordChangeModel
        {
            UserId = HttpContext.Name(),
        };

        return View("Views/User/ChangePassword.cshtml",model);
    }

    [HttpPost]
    public IActionResult ChangePassword(PasswordChangeModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.OldPassword == _userRepository.GetUserById(model.UserId).Password && model.Password == model.RepeatPassword)
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
                return View(model);
            }
        }
        else
        {
            return View(model);
        }
    }
}
