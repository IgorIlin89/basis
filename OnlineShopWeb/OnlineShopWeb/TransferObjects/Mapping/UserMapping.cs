using OnlineShopWeb.Domain;
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

    public static UserModel MapToModel(this User user)
    {
        return new UserModel
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
    }

    public static ICollection<UserModel> MapToModelList(this ICollection<User> userList) =>
        userList.Select(o => o.MapToModel()).ToList();
}
