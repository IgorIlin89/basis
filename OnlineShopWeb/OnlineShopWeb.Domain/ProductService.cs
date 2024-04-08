﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopWeb.Domain;

public class ProductService : IProductService
{
    private List<Product> ProductList = new List<Product>
    {
        new Product { ProductId = 0, Name="Persil", Producer="Henkel", Category= ProductCategory.Cleaning, Picture="Persil123.jpg"},
        new Product { ProductId = 1, Name="Weingummi", Producer="Haribo", Category= ProductCategory.Sweets, Picture="Haribo123.jpg"},
        new Product { ProductId = 2, Name="Pizza Salami", Producer="Dr. Oetker", Category= ProductCategory.Food, Picture="Pizza123.jpg"}
    };

    public List<Product> GetProductList() { return ProductList; }

    public Product? GetProduct(int productId)
    {
        return ProductList.Where(o => o.ProductId == productId).FirstOrDefault();
    }

    public bool Delete(int productId)
    {
        var productToDelete = ProductList.Where(o => o.ProductId == productId).FirstOrDefault();
        return ProductList.Remove(productToDelete);
    }

    public void AddProduct(int productId, string name, string producer, ProductCategory category, string picture)
    {
        ProductList.Add(new Product { ProductId = productId, Name = name, Producer = producer, Category=category, Picture = picture });
    }
}
