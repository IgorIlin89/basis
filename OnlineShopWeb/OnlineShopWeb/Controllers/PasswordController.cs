using OnlineShopWeb.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.TransferObjects.Dtos;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Json;
using System.Text;
using OnlineShopWeb.Misc;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.Controllers;

public class PasswordController : Controller
{
    private readonly IUserAdapter _userAdapter;

    public PasswordController(IUserAdapter userAdapter)
    {
        _userAdapter = userAdapter;
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
            var userDto = await _userAdapter.GetUserById(model.UserId.ToString());

            if (model.OldPassword == userDto.Password && model.Password == model.RepeatPassword)
            {
                var changePasswordDto = new ChangePasswordDto
                {
                    UserId = model.UserId,
                    Password = model.Password
                };

                var request = await _userAdapter.ChangeUserPassword(changePasswordDto);

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
