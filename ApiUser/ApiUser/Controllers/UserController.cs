using ApiUser.Application.Commands;
using ApiUser.Application.Handlers.Interfaces;
using ApiUser.Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace ApiUser.Controllers;

public class UserController(IGetUserListCommandHandler getUserListCommandHandler,
    IGetUserByEmailCommandHandler getUserByEmailCommandHandler,
    IGetUserByIdCommandHandler getUserByIdCommandHandler,
    IUpdateUserCommandHandler updateUserCommandHandler,
    IDeleteUserCommandHandler deleteUserCommandHandler,
    IAddUserCommandHandler addUserCommandHandler,
    IChangePasswordCommandHandler changePasswordCommandHandler) : ControllerBase
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
    public async Task<IActionResult> UpdateUser([FromBody] DtoUpdateUser updateUserDto)
    {
        var commmand = new UpdateUserCommand(updateUserDto);
        var user = updateUserCommandHandler.Handle(commmand);

        return Ok(user.MapToDto());
    }

    [Route("user/{id}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var command = new DeleteUserCommand(id.ToString());
        deleteUserCommandHandler.Handle(command);
        return Ok();
    }

    [Route("user")]
    [HttpPost]
    public async Task<IActionResult> AddUser([FromBody] DtoAddUser addUserDto)
    {
        var command = new AddUserCommand(addUserDto);
        var user = addUserCommandHandler.Handle(command);
        return Ok(user.MapToDto());
    }

    [Route("user/changepassword")]
    [HttpPost]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var command = new ChangePasswordCommand(changePasswordDto);
        var user = changePasswordCommandHandler.Handle(command);
        return Ok(user.MapToDto());
    }
}
