
namespace OnlineShopWeb.Domain;

public interface IUserService
{
    User? GetUser(int userId);
    List<User> GetUserList();
    public bool Delete(int userid);
    public void Add(int userid, string firstName, string lastName, int Age, string country, string city, string street, int postalCode);
}