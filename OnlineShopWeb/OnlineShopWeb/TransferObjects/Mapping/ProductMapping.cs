﻿using OnlineShopWeb.Domain;
using OnlineShopWeb.TransferObjects.Models;
using ProductCouponAdapter.DTOs;

namespace OnlineShopWeb.TransferObjects.Mapping;

public static class ProductMapping
{

    public static ProductCategoryModel MapToModel(this ProductCategory productCategory) =>
        productCategory switch
        {
            ProductCategory.Cleaning => ProductCategoryModel.Cleaning,
            ProductCategory.Sweets => ProductCategoryModel.Sweets,
            ProductCategory.Food => ProductCategoryModel.Food,
            ProductCategory.Electronics => ProductCategoryModel.Electronics,
            _ => throw new NotImplementedException()
        };

    public static ProductCategoryModel MapToModel(this ProductCategoryDto productCategory) =>
    productCategory switch
    {
        ProductCategoryDto.Cleaning => ProductCategoryModel.Cleaning,
        ProductCategoryDto.Sweets => ProductCategoryModel.Sweets,
        ProductCategoryDto.Food => ProductCategoryModel.Food,
        ProductCategoryDto.Electronics => ProductCategoryModel.Electronics,
        _ => throw new NotImplementedException()
    };

    public static ProductCategoryDto MapToModel(this ProductCategoryModel productCategory) =>
        productCategory switch
        {
            ProductCategoryModel.Cleaning => ProductCategoryDto.Cleaning,
            ProductCategoryModel.Sweets => ProductCategoryDto.Sweets,
            ProductCategoryModel.Food => ProductCategoryDto.Food,
            ProductCategoryModel.Electronics => ProductCategoryDto.Electronics,
            _ => throw new NotImplementedException(),
        };

    public static ProductModel MapToModel(this ProductDto productDto)
    {
        return new ProductModel
        {
            ProductId = productDto.ProductId,
            Name = productDto.Name,
            Producer = productDto.Producer,
            Category = productDto.Category.MapToModel(),
            Picture = productDto.Picture,
            Price = productDto.Price,
        };

    }

    public static ProductModel MapToModel(this Product product)
    {
        return new ProductModel
        {
            ProductId = product.Id,
            Name = product.Name,
            Producer = product.Producer,
            Category = product.Category.MapToModel(),
            Picture = product.Picture,
            Price = product.Price,
        };

    }

    public static IReadOnlyCollection<ProductModel> MapToModelList(this ICollection<Product> productList) =>
        productList.Select(o => o.MapToModel()).ToList();

}
