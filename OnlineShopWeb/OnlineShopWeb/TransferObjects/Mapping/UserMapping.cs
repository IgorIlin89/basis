using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Models;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class UserMapping
{
    public static UserModel MapToModel(this UserDto userDto)
    {
        return new UserModel
        {
            UserId = userDto.UserId,
            EMail = userDto.EMail,
            Password = userDto.Password,
            GivenName = userDto.GivenName,
            Surname = userDto.Surname,
            Age = userDto.Age,
            Country = userDto.Country,
            City = userDto.City,
            Street = userDto.Street,
            HouseNumber = userDto.HouseNumber,
            PostalCode = userDto.PostalCode,
        };
    }
}
