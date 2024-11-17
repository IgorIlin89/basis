namespace OnlineShopWeb.TransferObjects.Models.ListModels;

public class ProductListModel
{
    public IReadOnlyCollection<ProductModel> ProductModelList { get; set; } = new List<ProductModel>();
}
