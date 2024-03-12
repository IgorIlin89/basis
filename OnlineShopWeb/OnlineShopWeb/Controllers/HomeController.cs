using Microsoft.AspNetCore.Mvc;
using OnlineShopWeb.Domain;
using OnlineShopWeb.Models;
using System.Diagnostics;

namespace OnlineShopWeb.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISingletonSample _singletonSample;

    public HomeController(ILogger<HomeController> logger,
      ISingletonSample singletonSample)
    {
        _logger = logger;
        _singletonSample = singletonSample;
    }

    public IActionResult Index()
    {
        var model = new CounterModel
        {
            Counter = _singletonSample.Counter,
            ShowDefaultText = _singletonSample.Counter == 0
        };

        _singletonSample.Counter++;

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
