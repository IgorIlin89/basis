using Microsoft.AspNetCore.Mvc;
using OnlineShopWebAPI.Models;
using System.Diagnostics;
using OnlineShopWebAPI.Database;
using OnlineShopWebAPI.Domain;
using OnlineShopWebAPI.Database.Interfaces;

namespace OnlineShopWebAPI.Controllers;

public class APIController(IUserRepository _userRepositry) : ControllerBase
{
    [Route("api/userlist")]
    [HttpGet]
    public async Task<ActionResult<List<User>>> GetUserList()
    {
        var userList = _userRepositry.GetUserList();

        return Ok(userList);
    }

    [Route("api/user{id}")]
    [HttpGet]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = _userRepositry.GetUserById(id);

        return Ok(new UserModel
        {
            EMail = user.EMail,
            Password = user.Password,
            Name = user.Name
        });
    }
