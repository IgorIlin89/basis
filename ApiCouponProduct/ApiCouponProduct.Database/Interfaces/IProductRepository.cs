using ApiCouponProduct.Domain;

namespace ApiCouponProduct.Database.Interfaces;

public interface IProductRepository
{
    Product AddProduct(Product product);
    void Delete(int id);
    Product GetProductById(int id);
    List<Product> GetProductList();
    Product Update(Product product);
}