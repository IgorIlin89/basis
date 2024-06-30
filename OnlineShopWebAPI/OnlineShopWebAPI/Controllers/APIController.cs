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
    public async Task<ActionResult<List<User>>> GetUserList()
    {
        var userList = _userRepositry.GetUserList();

        return Ok(userList);
    }

    [Route("api/user{id}")]
    [HttpGet]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        //var user = _userRepositry.GetUserById(id);
        var httpClient = new HttpClient();

        var request = await httpClient.GetAsync("https://localhost:7216/api/userlist");
        var response = await request.Content.ReadAsStringAsync();

        List<User> userList = JsonSerializer.Deserialize<List<User>>(response);

        var user = userList.FirstOrDefault(o => o.Id == id);

        return Ok(new UserModel
        {
            EMail = user.EMail,
            Password = user.Password,
            Name = user.Name
        });
    }
}
