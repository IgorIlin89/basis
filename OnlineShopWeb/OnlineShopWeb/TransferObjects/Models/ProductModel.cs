namespace OnlineShopWeb.TransferObjects.Models;

public class ProductModel
{
    public int? ProductId { get; set; }
    public string Name { get; set; }
    public string Producer { get; set; }
    public ProductCategoryModel Category { get; set; }
    public string Picture { get; set; }
    public decimal Price { get; set; }
}
