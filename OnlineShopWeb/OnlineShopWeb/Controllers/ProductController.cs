using Microsoft.AspNetCore.Mvc;

namespace OnlineShopWeb.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
