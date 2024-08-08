namespace ApiUser.Domain.Dtos;

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

    public static User MapToUser(this UserDto userDto)
    {
        var user = new User();

        if (userDto.UserId is not null)
        {
            user.Id = userDto.UserId.Value;
        }

        user.EMail = userDto.EMail;
        user.Password = userDto.Password;
        user.GivenName = userDto.GivenName;
        user.Surname = userDto.Surname;
        user.Age = userDto.Age;
        user.Country = userDto.Country;
        user.City = userDto.City;
        user.Street = userDto.Street;
        user.HouseNumber = userDto.HouseNumber;
        user.PostalCode = userDto.PostalCode;

        return user;
    }

    public static List<UserDto> MapToDtoList(this List<User> userList)
    {
        List<UserDto> response = new List<UserDto>();

        foreach (var element in userList)
        {
            response.Add(element.MapToDto());
        }

        return response;
    }
}
