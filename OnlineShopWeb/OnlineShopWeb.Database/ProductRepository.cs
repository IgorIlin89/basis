﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using OnlineShopWeb.Database.Interfaces;
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

    public void EditProduct(Product product)
    {
        var entityEntry = GetProduct(product.Id);
        entityEntry.Name = product.Name;
        entityEntry.Producer = product.Producer;
        entityEntry.Category = product.Category;
        entityEntry.Picture = product.Picture;
        _dbContext.SaveChanges();
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

    public List<Product> GetProductList()
    {
        return _dbContext.Product.ToList();
    }
}