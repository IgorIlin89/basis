using ApiUser.Domain;
namespace ApiUser.Dtos;

public static class Mapping
{
    public static UserDto MapToDto(this User user)
    {
        return new UserDto
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

    public static List<UserDto> MapToDtoList(this List<User> userList)
    {
        List<UserDto> response = new List<UserDto>();

        foreach(var element in userList)
        {
            response.Add(element.MapToDto());
        }

        return response;
    }
}
