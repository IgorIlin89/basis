using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Database.Interfaces
{
    public interface IProductRepository
    {
        Product AddProduct(Product product);
        void DeleteProduct(int id);
        void EditProduct(Product product);
        Product? GetProductById(int id);
        List<Product> GetProductList();
    }
}