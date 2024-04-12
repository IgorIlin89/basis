using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class UserListModel
{
    public List<UserModel> UserModelList { get; set; } = new List<UserModel>();
}
