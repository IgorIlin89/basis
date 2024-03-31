using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productservice)
    {
        _productService = productservice;
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var product = _productService.GetProduct(id);

        var model = new ProductModel
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Producer = product.Producer,
            Category = product.Category,
            Picture = product.Picture,
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var product = _productService.GetProduct(id);

        var model = new ProductModel
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Producer = product.Producer,
            Category = product.Category,
            Picture = product.Picture,
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            var product = _productService.GetProduct(model.ProductId);

            product.ProductId = model.ProductId;
            product.Name = model.Name;
            product.Producer = model.Producer;
            product.Category = model.Category;
            product.Picture = model.Picture;
            return RedirectToAction("Index", "ProductList");
        }
        else
        {
            return View(model);
        }
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Add(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            _productService.AddProduct(model.ProductId, model.Name, model.Producer, model.Category, model.Picture);
            return RedirectToAction("Index", "ProductList");
        }
        else
        {
            return View(model);
        }
    }
}