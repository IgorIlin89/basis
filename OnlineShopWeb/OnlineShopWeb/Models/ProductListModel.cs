using OnlineShopWeb.Domain;
namespace OnlineShopWeb.Models;

public class ProductListModel
{
    public List<ProductModel> ProductModelList { get; set; } = new List<ProductModel>();
}
