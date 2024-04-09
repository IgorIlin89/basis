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
    public IActionResult Index()
    {
        var model = new ProductListModel
        {
            ProductList = _productService.GetProductList()
        };
        return View(model);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        _productService.Delete(id);
        return RedirectToAction("Index", "Product");
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
    public IActionResult Update(int? id)
    {
        var model = new ProductModel();

        if (id != null)
        {
            var product = _productService.GetProduct(id.Value);

            model.ProductId = product.ProductId;
            model.Name = product.Name;
            model.Producer = product.Producer;
            model.Category = product.Category;
            model.Picture = product.Picture;
        }


        return View(model);
    }


    [HttpPost]
    public IActionResult Update(ProductModel model)
    {
        if (ModelState.IsValid)
        {
            if (model.ProductId != null)
            {
                var product = _productService.GetProduct(model.ProductId.Value);

                product.Name = model.Name;
                product.Producer = model.Producer;
                product.Category = model.Category;
                product.Picture = model.Picture;
            }
            else
            {
                _productService.AddProduct(_productService.GetProductList().Count() + 1,
                    model.Name,
                    model.Producer,
                    model.Category,
                    model.Picture);
            }

            return RedirectToAction("Index", "Product");
        }
        else
        {
            return View(model);
        }
    }
}