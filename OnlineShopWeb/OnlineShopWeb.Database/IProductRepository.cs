using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database;

public interface IProductRepository
{
    List<Product> GetProducts();

    Product? GetProduct(int id);

    Product AddProduct(Product product);

    void DeleteProduct(int id);
    public Product Edit(Product product);
}