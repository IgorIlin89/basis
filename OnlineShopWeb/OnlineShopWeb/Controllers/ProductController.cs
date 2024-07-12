﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using System.Text.Json;
using System.Text;
using OnlineShopWeb.Dtos;
using static System.Net.Mime.MediaTypeNames;
using OnlineShopWeb.Misc;

namespace OnlineShopWeb.Controllers;

public class ProductController : Controller
{
    public HttpClientWrapper _httpClientWrapper;

    public ProductController(IConfiguration configuration
        , IHttpClientWrapper clientWrapper)
    {
        _httpClientWrapper = (HttpClientWrapper?)clientWrapper;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var productDtoList = await _httpClientWrapper.Get<List<ProductDto>>("productlist");
        var model = new ProductListModel();


        foreach (var productDto in productDtoList)
        {
            model.ProductModelList.Add(
                new ProductModel
                {
                    ProductId = productDto.ProductId,
                    Name = productDto.Name,
                    Producer = productDto.Producer,
                    Category = productDto.Category,
                    Picture = productDto.Picture,
                    Price = productDto.Price
                });
        }

        return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
        _httpClientWrapper.Delete<int>("productdelete", id);

        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var productDto = await _httpClientWrapper.Get<ProductDto>("getproductbyid", id);

        var model = new ProductModel
        {
            ProductId = productDto.ProductId,
            Name = productDto.Name.Trim(),
            Producer = productDto.Producer.Trim(),
            Category = productDto.Category,
            Picture = productDto.Picture.Trim(),
            Price = productDto.Price
        };

        return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Update(int? id)
    {
        var model = new ProductModel();

        if (id is not null)
        {
            var productDto = await _httpClientWrapper.Get<ProductDto>("getproductbyid", id.Value);

            model.ProductId = productDto.ProductId;
            model.Name = productDto.Name.Trim();
            model.Producer = productDto.Producer.Trim();
            model.Category = productDto.Category;
            model.Picture = productDto.Picture.Trim();
            model.Price = productDto.Price;
        }

        return View(model);
    }


    [HttpPost]
    public IActionResult Update(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.ProductId is not null)
            {
                var productToEdit = new ProductDto
                {
                    ProductId = model.ProductId.Value,
                    Name = model.Name,
                    Producer = model.Producer,
                    Category = model.Category,
                    Picture = model.Picture,
                    Price = model.Price
                };

                _httpClientWrapper.Put("productedit", productToEdit);
            }
            else
            {
                var productToAdd = new ProductDto
                {
                    Name = model.Name,
                    Producer = model.Producer,
                    Category = model.Category,
                    Picture = model.Picture,
                    Price = model.Price
                };

                var request = _httpClientWrapper.Post<ProductDto>("productadd", productToAdd);
            }

            return RedirectToAction("Index", "Product");
        }
        else
        {
            return View(model);
        }
    }
}