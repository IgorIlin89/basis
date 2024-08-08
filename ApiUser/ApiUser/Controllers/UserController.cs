using ApiUser.Application.Commands;
using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace ApiUser.Controllers;

public class UserController(IGetUserListCommandHandler getUserListCommandHandler,
    IGetUserByEmailCommandHandler getUserByEmailCommandHandler,
    IGetUserByIdCommandHandler getUserByIdCommandHandler,
    IUpdateUserCommandHandler updateUserCommandHandler) : ControllerBase
{
    [Route("user/list")]
    [HttpGet]
    public async Task<IActionResult> GetUserList()
    {
        var userList = getUserListCommandHandler.Handle();

        return Ok(userList.MapToDtoList());
    }

    [Route("user/{id}")]
    [HttpGet]
    public async Task<IActionResult> GetUserById(int id)
    {
        var command = new GetUserByIdCommand(id.ToString());
        var user = getUserByIdCommandHandler.Handle(command);

        return Ok(user.MapToDto());
    }

    [Route("user/email/{email}")]
    [HttpGet]
    public async Task<ActionResult> GetUserByEmail(string email)
    {
        var command = new GetUserByEmailCommand(email);
        var user = getUserByEmailCommandHandler.Handle(command);

        return Ok(user.MapToDto());
    }

    [Route("user")]
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
    {
        var commmand = new UpdateUserCommand(userDto);
        var user = updateUserCommandHandler.Handle(commmand);

        return Ok(user.MapToDto());
    }
}
