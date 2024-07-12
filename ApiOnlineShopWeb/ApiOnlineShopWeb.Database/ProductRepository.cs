using ApiOnlineShopWeb.Database.Interfaces;
using ApiOnlineShopWeb.Domain;

namespace ApiOnlineShopWeb.Database;

internal class ProductRepository : IProductRepository
{
    private ApiOnlineShopWebContext _dbContext;

    public ProductRepository(ApiOnlineShopWebContext onlineShopWebDbContext)
    {
        _dbContext = onlineShopWebDbContext;
    }

    public Product AddProduct(Product product)
    {
        _dbContext.Product.Add(product);
        _dbContext.SaveChanges();

        return product;
    }

    public void EditProduct(Product product)
    {
        var entityEntry = GetProductById(product.Id);
        entityEntry.Name = product.Name;
        entityEntry.Producer = product.Producer;
        entityEntry.Category = product.Category;
        entityEntry.Picture = product.Picture;
        entityEntry.Price = product.Price;
        _dbContext.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var entityEntry = GetProductById(id);
        _dbContext.Remove(entityEntry);
        _dbContext.SaveChanges();
    }

    public Product? GetProductById(int id)
    {
        return _dbContext.Product.FirstOrDefault(o => o.Id == id);
    }

    public Product? GetProductByName(string name)
    {
        return _dbContext.Product.FirstOrDefault(o => o.Name == name);
    }

    public List<Product> GetProductList()
    {
        return _dbContext.Product.ToList();
    }
}
