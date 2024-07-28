using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Dtos.Mapping;

public static class UserMapping
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
}
