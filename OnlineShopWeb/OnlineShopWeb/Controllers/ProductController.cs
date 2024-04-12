using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Database;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class ProductController : Controller
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var productList = _productRepository.GetProductList();
        var model = new ProductListModel();

        foreach (var product in productList)
        {
            model.ProductModelList.Add(
                new ProductModel
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Producer = product.Producer,
                    Category = product.Category,
                    Picture = product.Picture,
                });
        }

        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _productRepository.DeleteProduct(id);
        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        var product = _productRepository.GetProduct(id);

        var model = new ProductModel
        {
            ProductId = product.Id,
            Name = product.Name.Trim(),
            Producer = product.Producer.Trim(),
            Category = product.Category,
            Picture = product.Picture.Trim(),
        };

        return View(model);
    }

    [HttpGet]
    public IActionResult Update(int? id)
    {
        var model = new ProductModel();

        if (id is not null)
        {
            var product = _productRepository.GetProduct(id.Value);

            model.ProductId = product.Id;
            model.Name = product.Name.Trim();
            model.Producer = product.Producer.Trim();
            model.Category = product.Category;
            model.Picture = product.Picture.Trim();
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
                _productRepository.EditProduct(
                    new Product
                    {
                        Id = model.ProductId.Value,
                        Name = model.Name,
                        Producer = model.Producer,
                        Category = model.Category,
                        Picture = model.Picture,
                    });
            }
            else
            {
                _productRepository.AddProduct(
                    new Product
                    {
                        Name = model.Name,
                        Producer = model.Producer,
                        Category = model.Category,
                        Picture = model.Picture
                    }
                    );
            }

            return RedirectToAction("Index", "Product");
        }
        else
        {
            return View(model);
        }
    }
}