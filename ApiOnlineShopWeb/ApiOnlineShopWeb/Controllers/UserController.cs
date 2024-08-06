using Microsoft.AspNetCore.Mvc;
using ApiOnlineShopWeb.Domain;
using ApiOnlineShopWeb.Database.Interfaces;
using System.Text.Json;
using ApiOnlineShopWeb.Dtos;
using System.Xml.Linq;
using ApiOnlineShopWeb.Dtos.Mapping;

namespace ApiOnlineShopWeb.Controllers;

public class UserController(IUserRepository _userRepository) : ControllerBase
{
    [Route("user/list")]
    [HttpGet]
    public async Task<ActionResult> GetUserList()
    {
        var userList = _userRepository.GetUserList();

        if (userList == null)
        {
            return NotFound();
        }

        List<UserDto> response = new List<UserDto>();

        foreach (var element in userList)
        {
            response.Add(element.MapToDto());
        }

        return Ok(response);
    }

    [Route("user/{id}")]
    [HttpGet]
    public async Task<ActionResult> GetUserById(int id)
    {
        var user = _userRepository.GetUserById(id);

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user.MapToDto());
    }

    [Route("user/email/{email}")]
    [HttpGet]
    public async Task<ActionResult> GetUserByEmail(string email)
    {
        var user = _userRepository.GetUserByEMail(email);

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

        _userRepository.Update(user);

        return Ok();
    }

    [Route("user/{id}")]
    [HttpDelete]
    public async Task<ActionResult> DeleteUser(int id)
    {
        _userRepository.Delete(id);
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

        var newUser = _userRepository.AddUser(userToAdd);

        return Ok(newUser.MapToDto());
    }

    [Route("user/changepassword")]
    [HttpPost]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        _userRepository.ChangePassword(changePasswordDto.UserId, changePasswordDto.Password);
        return Ok();
    }

}
