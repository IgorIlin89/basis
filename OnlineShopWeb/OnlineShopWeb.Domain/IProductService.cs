
namespace OnlineShopWeb.Domain;

public interface IProductService
{
    void AddProduct(int productId, string name, string producer, ProductCategory category, string picture);
    bool Delete(int productId);
    Product? GetProduct(int productId);
    List<Product> GetProductList();
}