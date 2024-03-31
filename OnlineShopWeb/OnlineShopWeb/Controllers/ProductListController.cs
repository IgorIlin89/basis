using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;

namespace OnlineShopWeb.Controllers;

public class ProductListController : Controller
{
    private readonly IProductService _productService;

    public ProductListController(IProductService productService)
    {
        _productService = productService;
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
        return RedirectToAction("Index", "ProductList");
    }
}
