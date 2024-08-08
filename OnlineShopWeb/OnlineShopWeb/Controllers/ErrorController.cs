using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWeb.Controllers;

public class ErrorController : Controller
{
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return View("~/Views/Shared/DefaultErrorPage.cshtml");
    }
}
