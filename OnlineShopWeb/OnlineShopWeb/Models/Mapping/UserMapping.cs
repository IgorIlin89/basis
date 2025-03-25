using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Models.Mapping;
public static class UserMapping
{
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

    public static IReadOnlyCollection<UserModel> MapToModelList(this IReadOnlyCollection<User> userList) =>
        userList.Select(o => o.MapToModel()).ToList();
}
