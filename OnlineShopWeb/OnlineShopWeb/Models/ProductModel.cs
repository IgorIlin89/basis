using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Models;

public class ProductModel
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Producer { get; set; }
    public EProductCategorys Category { get; set; }
    public string Picture { get; set; }
    //List<string> Tags { get; set; }
}
