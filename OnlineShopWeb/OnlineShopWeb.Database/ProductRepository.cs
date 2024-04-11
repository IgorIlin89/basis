using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShopWeb.Domain;

namespace OnlineShopWeb.Database;

internal class ProductRepository : IProductRepository
{
    private OnlineShopWebDbContext _dbContext;

    public ProductRepository(OnlineShopWebDbContext onlineShopWebDbContext)
    {
        _dbContext = onlineShopWebDbContext;
    }

    public Product AddProduct(Product product)
    {
        _dbContext.Product.Add(product);
        _dbContext.SaveChanges();

        return product;
    }

    public Product Edit(Product product)
    {
        var entityEntry = GetProduct(product.Id);
        entityEntry.Name = product.Name;
        entityEntry.Producer = product.Producer;
        entityEntry.Category = product.Category;
        entityEntry.Picture = product.Picture;
        _dbContext.SaveChanges();
        return new Product();
    }

    public void DeleteProduct(int id)
    {
        var entityEntry = GetProduct(id);
        _dbContext.Remove(entityEntry);
        _dbContext.SaveChanges();
    }

    public Product? GetProduct(int id)
    {
        return _dbContext.Product.FirstOrDefault(o => o.Id == id);
    }

    public List<Product> GetProducts()
    {
        return _dbContext.Product.ToList();
    }
}