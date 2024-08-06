using Microsoft.AspNetCore.Mvc;
namespace ApiUser.Controllers;

public class UserController : ControllerBase
{
    [Route("user/list")]
    [HttpGet]
    public async Task<IActionResult> GetUserList()
    {
        //Create Dto here to return to ShopWeb
        return Ok();
    }

    public async Task<IActionResult> AddUser()
    {
        // you get dto here, convert to command 
        return Ok();
    }
}
