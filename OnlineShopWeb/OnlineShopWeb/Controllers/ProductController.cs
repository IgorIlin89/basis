using Microsoft.AspNetCore.Mvc;
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
    public IHttpClientWrapper _httpClientWrapper;

    public ProductController(IHttpClientWrapper clientWrapper)
    {
        _httpClientWrapper = clientWrapper;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var productDtoList = await _httpClientWrapper.Get<List<ProductDto>>("product", "list");

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
        _httpClientWrapper.Delete("product", id.ToString());

        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var productDto = await _httpClientWrapper.Get<ProductDto>("product", id.ToString());

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
            var productDto = await _httpClientWrapper.Get<ProductDto>("product", id.ToString());

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

                _httpClientWrapper.Put<ProductDto>("product", productToEdit);
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

                var request = _httpClientWrapper.Post<ProductDto, ProductDto>("product", productToAdd);
            }

            return RedirectToAction("Index", "Product");
        }
        else
        {
            return View(model);
        }
    }
}