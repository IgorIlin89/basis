using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database.Interfaces;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using System.Text.Json;
using System.Text;
using OnlineShopWeb.Dtos;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineShopWeb.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;
    private readonly HttpClient _httpClient = new HttpClient();
    private readonly string _connectionString;
    private readonly string _connectToGetProductList;
    public readonly string _connectToGetProductById;
    public readonly string _connectToDeleteProduct;
    public readonly string _connectToEditProduct;
    public readonly string _connectToAddProduct;

    public ProductController(IProductRepository productRepository,
        IConfiguration configuration)
    {
        _productRepository = productRepository;
        _connectionString = configuration.GetConnectionString("ApiURL");
        _connectToGetProductList = configuration.GetConnectionString("ApiProductControllerGetProductList");
        _connectToGetProductById = configuration.GetConnectionString("ApiProductControllerGetProductById");
        _connectToDeleteProduct = configuration.GetConnectionString("ApiProductControllerDeleteProduct");
        _connectToEditProduct = configuration.GetConnectionString("ApiProductControllerEditProduct");
        _connectToAddProduct = configuration.GetConnectionString("ApiProductControllerAddProduct");
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var request = await _httpClient.GetAsync(_connectionString + _connectToGetProductList);
        var response = await request.Content.ReadAsStringAsync();

        var productDtoList = JsonSerializer.Deserialize<List<ProductDto>>(response);
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
        var request = await _httpClient.GetAsync(_connectionString + _connectToDeleteProduct + id);
        //var response = await request.Content.ReadAsStringAsync();

        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var request = await _httpClient.GetAsync(_connectionString + _connectToGetProductById + id.ToString());
        var response = await request.Content.ReadAsStringAsync();

        var productDto = JsonSerializer.Deserialize<ProductDto>(response);

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
            var request = await _httpClient.GetAsync(_connectionString + _connectToGetProductById + id.ToString());
            var response = await request.Content.ReadAsStringAsync();

            var product = JsonSerializer.Deserialize<ProductDto>(response);

            model.ProductId = product.ProductId;
            model.Name = product.Name.Trim();
            model.Producer = product.Producer.Trim();
            model.Category = product.Category;
            model.Picture = product.Picture.Trim();
            model.Price = product.Price;
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

                var httpBody = new StringContent(
                    JsonSerializer.Serialize(productToEdit),
                    Encoding.UTF8,
                    Application.Json
                    );

                var request = _httpClient.PostAsync(_connectionString + _connectToEditProduct, 
                    httpBody);
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

                var httpBody = new StringContent(
                    JsonSerializer.Serialize(productToAdd),
                    Encoding.UTF8,
                    Application.Json);

                var request = _httpClient.PostAsync(_connectionString + _connectToAddProduct,
                    httpBody);

            }

            return RedirectToAction("Index", "Product");
        }
        else
        {
            return View(model);
        }
    }
}