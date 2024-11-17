using OnlineShopWeb.Domain;
using UserAdapter.DTOs;

namespace UserAdapter.Mapping;

public static class UserMapping
{
    public static UserDto MapToDto(this User user) =>
        new UserDto
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

    public static User MapToUser(this UserDto userDto) =>
        new User
        {
            Id = userDto.UserId is null ? 0 : userDto.UserId.Value,
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

    public static List<User> MapToUserList(this List<UserDto> userDto) =>
        userDto.Select(o => o.MapToUser()).ToList();
}
