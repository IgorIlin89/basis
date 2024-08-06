using OnlineShopWeb.Domain;

namespace OnlineShopWeb.TransferObjects.Models.ListModels;

public class ProductListModel
{
    public List<ProductModel> ProductModelList { get; set; } = new List<ProductModel>();
}
