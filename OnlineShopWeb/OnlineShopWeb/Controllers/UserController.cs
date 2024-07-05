using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Models;
using OnlineShopWeb.Dtos;
using System.Text;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Controllers;
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _connectionString;
    private readonly string _connectToGetUserList;
    private readonly string _connectToDeleteUser;
    private readonly string _connectToGetUserById;
    private readonly string _connectToUpdateUser;


    public UserController(IUserRepository userRepository,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _connectionString = configuration.GetConnectionString("ApiURL");
        _connectToGetUserList = configuration.GetConnectionString("ApiUserControllerGetUserList");
        _connectToDeleteUser = configuration.GetConnectionString("ApiUserControllerDeleteUser");
        _connectToGetUserById = configuration.GetConnectionString("ApiUserControllerGetUserById");
        _connectToUpdateUser = configuration.GetConnectionString("ApiUserControllerUpdateUser");
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var request = await _httpClient.GetAsync(_connectionString + _connectToGetUserList);
        var response = await request.Content.ReadAsStringAsync();

        var userDtoList = JsonSerializer.Deserialize<List<UserDto>>(response);

        var model = new UserListModel();

        foreach (var userDto in userDtoList)
        {
            model.UserModelList.Add(
                new UserModel
                {
                    UserId = userDto.UserId,
                    EMail = userDto.EMail,
                    GivenName = userDto.GivenName,
                    Surname = userDto.Surname,
                    Age = userDto.Age,
                    Country = userDto.Country,
                    City = userDto.City,
                    Street = userDto.Street,
                    HouseNumber = userDto.HouseNumber,
                    PostalCode = userDto.PostalCode,
                });
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var request = await _httpClient.GetAsync(_connectionString + _connectToDeleteUser + id);
        return RedirectToAction("Index", "User");
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id)
    {

        var request = await _httpClient.GetAsync(_connectionString + _connectToGetUserById + id);
        var response = await request.Content.ReadAsStringAsync();

        var userDto = JsonSerializer.Deserialize<UserDto>(response);

        var model = new UserModel
        {
            UserId = userDto.UserId,
            EMail = userDto.EMail.Trim(),
            GivenName = userDto.GivenName.Trim(),
            Surname = userDto.Surname.Trim(),
            Age = userDto.Age,
            Country = userDto.Country.Trim(),
            City = userDto.City.Trim(),
            Street = userDto.Street.Trim(),
            HouseNumber = userDto.HouseNumber,
            PostalCode = userDto.PostalCode
        };

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update(int? id)
    {
        var model = new UserModel();

        if (HttpContext.User.Identity.Name is not null)
        {
            var request = await _httpClient.GetAsync(_connectionString + _connectToGetUserById + id);
            var response = await request.Content.ReadAsStringAsync();

            var userDto = JsonSerializer.Deserialize<UserDto>(response);

            model.UserId = userDto.UserId;
            model.EMail = userDto.EMail.Trim();
            model.GivenName = userDto.GivenName.Trim();
            model.Surname = userDto.Surname.Trim();
            model.Age = userDto.Age;
            model.Country = userDto.Country.Trim();
            model.City = userDto.City.Trim();
            model.Street = userDto.Street.Trim();
            model.HouseNumber = userDto.HouseNumber;
            model.PostalCode = userDto.PostalCode;

        }

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update(UserModel model)
    {
        if (ModelState.IsValid)
        {
            var userToUpdate = new UserDto
            {
                UserId = model.UserId,
                EMail = model.EMail.Trim(),
                GivenName = model.GivenName.Trim(),
                Surname = model.Surname.Trim(),
                Age = model.Age,
                Country = model.Country.Trim(),
                City = model.City.Trim(),
                Street = model.Street.Trim(),
                HouseNumber = model.HouseNumber,
                PostalCode = model.PostalCode
            };

            var httpBody = new StringContent(
                    JsonSerializer.Serialize(userToUpdate),
                    Encoding.UTF8,
                    Application.Json);

            _httpClient.PostAsync(_connectionString + _connectToUpdateUser, httpBody);

            return RedirectToAction("Index", "User");
        }
        else
        {
            return View(model);
        }
    }
}
