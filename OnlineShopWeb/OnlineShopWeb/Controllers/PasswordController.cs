using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class PasswordController : Controller
{
    private readonly IUserRepository _userRepository;

    public PasswordController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    //It doesnt work with the HttpGet, why?
    //[HttpGet]
    public IActionResult ChangePassword()
    {
        var model = new PasswordChangeModel
        {
            UserId = Int32.Parse(HttpContext.User.Identity.Name),
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult ChangePassword(PasswordChangeModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.OldPassword == _userRepository.GetUserById(model.UserId).Password && model.Password == model.RepeatPassword)
            {
                _userRepository.ChangePassword(model.UserId, model.Password);
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
