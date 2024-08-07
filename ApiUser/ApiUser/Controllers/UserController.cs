using Microsoft.AspNetCore.Mvc;
using ApiUser.Database.Interfaces;
using ApiUser.Domain;
using ApiUser.Dtos;
using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Application.Commands;
namespace ApiUser.Controllers;

public class UserController (IGetUserListCommandHandler getUserListCommandHandler,
    IGetUserByEmailCommandHandler getUserByEmailCommandHandler) : ControllerBase
{
    [Route("user/list")]
    [HttpGet]
    public async Task<IActionResult> GetUserList()
    {
        var userList = getUserListCommandHandler.Handle();

        return Ok(userList.MapToDtoList());
    }

    [Route("user/email/{email}")]
    [HttpGet]
    public async Task<ActionResult> GetUserByEmail(string email)
    {
        var command = new GetUserByEmailCommand(email);

        var user = getUserByEmailCommandHandler.Handle(command);

        return Ok(user.MapToDto);
    }
}
