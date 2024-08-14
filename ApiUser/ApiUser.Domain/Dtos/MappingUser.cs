namespace ApiUser.Domain.Dtos;

public static class MappingUser
{
    public static DtoUpdateUser MapToDto(this User user)
    {
        return new DtoUpdateUser
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

    public static User MapToUser(this DtoUpdateUser userDto)
    {
        var user = new User();

        user.Id = userDto.UserId;
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

    public static User MapToUser(this DtoAddUser userDto)
    {
        var user = new User();

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

    public static List<DtoUpdateUser> MapToDtoList(this List<User> userList)
    {
        List<DtoUpdateUser> response = new List<DtoUpdateUser>();

        foreach (var element in userList)
        {
            response.Add(element.MapToDto());
        }

        return response;
    }
}
