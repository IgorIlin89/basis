
namespace OnlineShopWeb.Domain
{
    public interface IUserService
    {
        User? GetUser(int userId);
        List<User> GetUserList();
        public bool Delete(int userid);
    }
}