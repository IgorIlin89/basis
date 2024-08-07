using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Misc;
using OnlineShopWeb.Adapters.Interfaces;
using OnlineShopWeb.TransferObjects.Models;
using OnlineShopWeb.TransferObjects.Models.ListModels;
using OnlineShopWeb.TransferObjects.Dtos;
using OnlineShopWeb.TransferObjects.Mapping;

namespace OnlineShopWeb.Controllers;

public class ErrorController : Controller
{
    [HttpGet]
    [Route("errorview")]
    public IActionResult Index()
    {
        return View("Views/Shared/DefaultErrorPage");
    }
}
