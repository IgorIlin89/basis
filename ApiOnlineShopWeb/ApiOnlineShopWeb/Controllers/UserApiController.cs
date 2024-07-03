using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ApiOnlineShopWeb.Domain;
using ApiOnlineShopWeb.Database.Interfaces;
using System.Text.Json;
using ApiOnlineShopWeb.Dtos;

namespace OnlineShopWebAPI.Controllers;

public class UserApiController(IUserRepository _userRepositry) : ControllerBase
{
    [Route("userlist")]
    [HttpGet]
    public async Task<ActionResult> GetUserList()
    {
        var userList = _userRepositry.GetUserList();

        List<UserDto> userListDto = new List<UserDto>();

        foreach (var element in userList)
        {
            userListDto.Add(new UserDto
            {
                EMail = element.EMail,
                Name = element.Name
            });
        }

        var response = JsonSerializer.Serialize(userListDto);

        return Ok(response);
    }

    [Route("user{id}")]
    [HttpGet]
    public async Task<ActionResult> GetUser(int id)
    {
        var user = _userRepositry.GetUserById(id);

        var response = JsonSerializer.Serialize(new UserDto
        {
            Id = user.Id,
            EMail = user.EMail,
            Name = user.Name
        });

        return Ok(response);
    }

    [Route("userupdate")]
    [HttpPost]
    public async Task<ActionResult> UpdateUser([FromBody] UserDto userDto)
    {
        var user = new User
        {
            Id = userDto.Id,
            EMail = userDto.EMail,
            Name = userDto.Name
        };

        _userRepositry.Update(user);
        return Ok();
    }

}
