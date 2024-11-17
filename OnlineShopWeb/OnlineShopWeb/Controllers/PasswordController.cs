using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.ExtensionMethods;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.Controllers;

public class PasswordController(IGetUserByIdCommandHandler getUserByIdCommandHandler,
    IChangeUserPasswordCommandHandler changeUserPasswordCommandHandler) : Controller
{
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
            var command = new GetUserByIdCommand(model.UserId.ToString());

            var user = await getUserByIdCommandHandler.Handle(command);

            if (model.OldPassword == user.Password && model.Password == model.RepeatPassword)
            {
                var commandChangePassword = new ChangeUserPasswordCommand(model.UserId.ToString(), model.Password);
                var changedPassword = await changeUserPasswordCommandHandler.Handle(commandChangePassword);

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
