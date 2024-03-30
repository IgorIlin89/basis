using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

public class ProductService
{
    private List<Product> ProductList = new List<Product>
    {
        new Product { ProductId = 0, Name="Persil", Producer="Henkel", Category="Cleaning", Picture="Persil123"},
        new Product { ProductId = 1, Name="Weingummi", Producer="Haribo", Category="Sweets", Picture="Haribo123"},
        new Product { ProductId = 2, Name="Pizza Salami", Producer="Dr. Oetker", Category="Food", Picture="Pizza123"}
    };

    public List<Product> GetProductList() {  return ProductList; }

    public Product? GetProduct(int productId)
    {
        return ProductList.Where(o => o.ProductId == productId).FirstOrDefault();
    }

    public bool Delete(int productId)
    {
        var productToDelete = ProductList.Where(o => o.ProductId == productId).FirstOrDefault();
        return ProductList.Remove(productToDelete);
    }

    public bool Delete(string name)
    {
        var productToDelete = ProductList.Where(o => o.Name == name).FirstOrDefault();
        return ProductList.Remove(productToDelete);
    }
}
