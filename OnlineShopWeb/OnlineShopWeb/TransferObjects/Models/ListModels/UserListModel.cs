using OnlineShopWeb.Domain;

namespace OnlineShopWeb.TransferObjects.Models.ListModels;

public class UserListModel
{
    public List<UserModel> UserModelList { get; set; } = new List<UserModel>();
}
