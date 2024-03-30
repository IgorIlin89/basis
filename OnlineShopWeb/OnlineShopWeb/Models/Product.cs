namespace OnlineShopWeb.Models;

public class Product
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Producer { get; set; }
    public string Category { get; set; }
    public string Picture { get; set; }
    List<string> Tags { get; set; }
}
