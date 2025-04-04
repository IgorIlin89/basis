﻿using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Application.Commands.Product;
using OnlineShopWeb.Application.Interfaces;
using OnlineShopWeb.Models;
using OnlineShopWeb.Models.Mapping;

namespace OnlineShopWeb.Controllers;

public class ProductController(IGetProductByIdCommandHandler getProductByIdCommandHandler,
    IGetProductListCommandHandler getProductListCommandHandler,
    IProductAddCommandHandler productAddCommandHandler,
    IProductDeleteCommandHandler productDeleteCommandHandler,
    IProductUpdateCommandHandler productUpdateCommandHandler) : Controller
{
    [HttpGet]
    public async Task<ActionResult> Index()
    {
        // var accessToken = await HttpContext.Authentication.GetTokenAsync("access_token");

        var command = new GetProductListCommand();
        var productList = await getProductListCommandHandler.Handle(command);

        var model = productList.MapToModelList();

        //TODO Tests have to mock HttpClient, GRPC Client, NServiceBus, basically all outpuut adapters+

        return View(model);
    }

    [HttpGet]
    public async Task<ActionResult> Delete(int id)
    {
        var command = new ProductDeleteCommand(id.ToString());
        productDeleteCommandHandler.Handle(command);

        return RedirectToAction("Index", "Product");
    }

    [HttpGet]
    public async Task<ActionResult> Details(int id)
    {
        var command = new GetProductByIdCommand(id.ToString());
        var product = await getProductByIdCommandHandler.Handle(command);

        return View(product.MapToModel());
    }

    [HttpGet]
    public async Task<ActionResult> Update(int? id)
    {
        var model = new ProductModel();

        if (id is not null)
        {
            var command = new GetProductByIdCommand(id.ToString());
            var product = await getProductByIdCommandHandler.Handle(command);

            model = product.MapToModel();
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
                var commandToUpdate = new ProductUpdateCommand(model.ProductId.Value,
                    model.Name, model.Producer, model.Category.MapToDto(), model.Picture,
                    model.Price);

                var product = await productUpdateCommandHandler.Handle(commandToUpdate);
            }
            else
            {
                var commandToAdd = new ProductAddCommand(null,
                    model.Name, model.Producer, model.Category.MapToDto(), model.Picture,
                    model.Price);

                var product = await productAddCommandHandler.Handle(commandToAdd);
            }

            return RedirectToAction("Index", "Product");
        }
        else
        {
            return View(model);
        }
    }
}