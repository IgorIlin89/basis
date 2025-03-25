using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.User;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Models;
using OnlineShopWeb.Models.Mapping;

namespace OnlineShopWeb.Controllers;
public class UserController(IGetUserListCommandHandler getUserListCommandHandler,
    IGetUserByEmailCommandHandler getUserByEmailCommandHandler,
    IGetUserByIdCommandHandler getUserByIdCommandHandler,
    IUserAddCommandHandler userAddCommandHandler,
    IUserDeleteCommandHandler userDeleteCommandHandler,
    IUserUpdateCommandHandler userUpdateCommandHandler) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var command = new GetUserListCommand();
        var userList = await getUserListCommandHandler.Handle(command);

        return View(userList.MapToModelList());
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        var command = new UserDeleteCommand(id.ToString());
        userDeleteCommandHandler.Handle(command);

        return RedirectToAction("Index", "User");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {
        var command = new GetUserByIdCommand(id.ToString());
        var user = await getUserByIdCommandHandler.Handle(command);

        return View(user.MapToModel());
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var model = new UserModel();

        var command = new GetUserByIdCommand(id.ToString());
        var user = await getUserByIdCommandHandler.Handle(command);
        model = user.MapToModel();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UserModel model)
    {
        if (ModelState.IsValid)
        {

            var command = new UserUpdateCommand(model.UserId.ToString(), model.EMail,
                model.GivenName, model.Surname, model.Age, model.Country, model.City,
                model.Street, model.HouseNumber, model.PostalCode);

            var user = await userUpdateCommandHandler.Handle(command);

            return RedirectToAction("Index", "User");
        }
        else
        {
            return View(model);
        }
    }
}
