using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class UserMapping
{
    public static UserModel MapToModel(this UserDto userDto)
    {
        var userModel = new UserModel();

        if (userDto.UserId is not null)
        {
            userModel.UserId = userDto.UserId;
        }

        if (userDto.Password is not null)
        {
            userModel.Password = userDto.Password;
        }

        userModel.EMail = userDto.EMail;
        userModel.GivenName = userDto.GivenName;
        userModel.Surname = userDto.Surname;
        userModel.Age = userDto.Age;
        userModel.Country = userDto.Country;
        userModel.City = userDto.City;
        userModel.Street = userDto.Street;
        userModel.HouseNumber = userDto.HouseNumber;
        userModel.PostalCode = userDto.PostalCode;

        return userModel;
    }
}
