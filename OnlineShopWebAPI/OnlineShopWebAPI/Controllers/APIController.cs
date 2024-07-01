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
        var response = JsonSerializer.Serialize(userList);

        return Ok(response);
    }

    [Route("api/user{id}")]
    [HttpGet]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = _userRepositry.GetUserById(id);

        var response = JsonSerializer.Serialize(user);

        return Ok(response);
    }

    [Route("api/userupdate")]
    [HttpPost]
    public async Task<ActionResult> UpdateUser([FromBody]User user)
    {
        _userRepositry.Update(user);
        return Ok();
    }

}
