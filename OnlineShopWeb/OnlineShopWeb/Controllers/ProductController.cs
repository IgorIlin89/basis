using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Misc;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.TransferObjects.Models;
using OnlineShopWeb.TransferObjects.Models.ListModels;
using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Mapping;

namespace OnlineShopWeb.Controllers;

public class ProductController : Controller
{
    private readonly IProductCouponAdapter _productCouponAdapter;

    public ProductController(IProductCouponAdapter productCouponAdapter)
    {
        _productCouponAdapter = productCouponAdapter;
    }

    [HttpGet]
    public async Task<ActionResult> Index()
    {
        var productDtoList = await _productCouponAdapter.GetProductList();

        var model = new ProductListModel();

        foreach (var element in productDtoList)
        {
            model.ProductModelList.Add(element.MapToModel());
        }

        return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
        _productCouponAdapter.ProductDelete(id.ToString());

        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var productDto = await _productCouponAdapter.GetProductById(id.ToString());

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
            var productDto = await _productCouponAdapter.GetProductById(id.ToString());

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
    public async Task<IActionResult> Update(ProductModel model)
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

                await _productCouponAdapter.ProductUpdate(productToEdit);
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

                await _productCouponAdapter.ProductAdd(productToAdd);
            }

            return RedirectToAction("Index", "Product");
        }
        else
        {
            return View(model);
        }
    }
}