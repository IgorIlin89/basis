using ApiUser.Application.Commands;
using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace ApiUser.Controllers;

public class UserController(IGetUserListCommandHandler getUserListCommandHandler,
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

        return Ok(user.MapToDto());
    }
}
