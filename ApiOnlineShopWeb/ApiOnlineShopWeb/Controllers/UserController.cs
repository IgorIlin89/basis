using Microsoft.AspNetCore.Mvc;
using ApiOnlineShopWeb.Domain;
using ApiOnlineShopWeb.Database.Interfaces;
using System.Text.Json;
using ApiOnlineShopWeb.Dtos;
using System.Xml.Linq;

namespace ApiOnlineShopWeb.Controllers;

public class UserController(IUserRepository _userRepositry) : ControllerBase
{
    [Route("user/list")]
    [HttpGet]
    public async Task<ActionResult> GetUserList()
    {
        var userList = _userRepositry.GetUserList();

        if (userList == null)
        {
            return NotFound();
        }

        List<UserDto> response = new List<UserDto>();

        foreach (var element in userList)
        {
            response.Add(new UserDto
            {
                UserId = element.Id,
                EMail = element.EMail,
                GivenName = element.GivenName,
                Surname = element.Surname,
                Age = element.Age,
                Country = element.Country,
                City = element.City,
                Street = element.Street,
                HouseNumber = element.HouseNumber,
                PostalCode = element.PostalCode,
            });
        }

        return Ok(response);
    }

    [Route("user/{id}")]
    [HttpGet]
    public async Task<ActionResult> GetUserById(int id)
    {
        var user = _userRepositry.GetUserById(id);

        if (user == null)
        {
            return NotFound();
        }

        var response = new UserDto
        {
            UserId = user.Id,
            EMail = user.EMail,
            Password = user.Password,
            GivenName = user.GivenName,
            Surname = user.Surname,
            Age = user.Age,
            Country = user.Country,
            City = user.City,
            Street = user.Street,
            HouseNumber = user.HouseNumber,
            PostalCode = user.PostalCode,
        };

        return Ok(response);
    }

    [Route("user/email/{email}")]
    [HttpGet]
    public async Task<ActionResult> GetUserByEmail(string email)
    {
        var user = _userRepositry.GetUserByEMail(email);

        if(user == null)
        {
            return NotFound();
        }

        var response = new UserDto
        {
            UserId = user.Id,
            EMail = user.EMail,
            Password = user.Password,
            GivenName = user.GivenName,
            Surname = user.Surname,
            Age = user.Age,
            Country = user.Country,
            City = user.City,
            Street = user.Street,
            HouseNumber = user.HouseNumber,
            PostalCode = user.PostalCode,
        };

        return Ok(response);
    }

    [Route("user")]
    [HttpPut]
    public async Task<ActionResult> UpdateUser([FromBody] UserDto userDto)
    {
        var user = new User
        {
            Id = userDto.UserId.Value,
            EMail = userDto.EMail,
            GivenName = userDto.GivenName,
            Surname = userDto.Surname,
            Age = userDto.Age,
            Country = userDto.Country,
            City = userDto.City,
            Street = userDto.Street,
            HouseNumber = userDto.HouseNumber,
            PostalCode = userDto.PostalCode,
            Password = userDto.Password
        };

        _userRepositry.Update(user);

        return Ok();
    }

    [Route("user/{id}")]
    [HttpDelete]
    public async Task<ActionResult> DeleteUser(int id)
    {
        _userRepositry.Delete(id);
        return Ok();
    }

    [Route("user")]
    [HttpPost]
    public async Task<ActionResult> AddUser([FromBody] UserDto userDto)
    {
        var userToAdd = new User
        {
            EMail = userDto.EMail,
            GivenName = userDto.GivenName,
            Surname = userDto.Surname,
            Age = userDto.Age,
            Country = userDto.Country,
            City = userDto.City,
            Street = userDto.Street,
            HouseNumber = userDto.HouseNumber,
            PostalCode = userDto.PostalCode,
            Password = userDto.Password
        };

        _userRepositry.AddUser(userToAdd);

        return Ok();
    }

    [Route("user/changepassword")]
    [HttpPost]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        _userRepositry.ChangePassword(changePasswordDto.UserId, changePasswordDto.Password);
        return Ok();
    }

}
