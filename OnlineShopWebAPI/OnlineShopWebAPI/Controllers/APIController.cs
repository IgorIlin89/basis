using Microsoft.AspNetCore.Mvc;
using OnlineShopWebAPI.Models;
using System.Diagnostics;
using OnlineShopWebAPI.Database;
using OnlineShopWebAPI.Domain;
using OnlineShopWebAPI.Database.Interfaces;
using System.Text.Json;

namespace OnlineShopWebAPI.Controllers;

public class APIController(IUserRepository _userRepositry) : ControllerBase
{
    [Route("api/userlist")]
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

    [Route("api/user{id}")]
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

    [Route("api/userupdate")]
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
